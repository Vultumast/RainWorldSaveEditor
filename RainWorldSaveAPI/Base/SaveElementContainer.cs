using System.Reflection;
using System.Text;

namespace RainWorldSaveAPI.Base;

public abstract class SaveElementContainer
{
    private struct FieldSerializationData
    {
        public Serializers.PropSerializer? Serializer;
        public Serializers.PropDeserializer? Deserializer;
        public SaveFieldAttribute Metadata;
        public PropertyInfo Prop;
    }

    private static Dictionary<Type, Dictionary<string, FieldSerializationData>> SerializationData { get; } = [];

    public List<(string, string[])> UnrecognizedFields { get; protected set; } = [];

    private const int MaxDebugChars = 50;

    public SaveElementContainer()
    {
        CheckInit(GetType());
    }

    private static Serializers.PropDeserializer? GetDeserializer(PropertyInfo prop)
    {
        try
        {
            return Serializers.GenerateDeserializer(prop);
        }
        catch (Exception e)
        {
            Logger.Warn($"{prop.PropertyType}: failed to get deserializer.");
            Logger.Warn(e.ToString());
            return null;
        }
    }

    private static Serializers.PropSerializer? GetSerializer(PropertyInfo prop)
    {
        try
        {
            return Serializers.GenerateSerializer(prop);
        }
        catch (Exception e)
        {
            Logger.Warn($"{prop.PropertyType}: failed to get serializer.");
            Logger.Warn(e.ToString());
            return null;
        }
    }

    protected virtual void DeserializeUnrecognizedField(string key, string[] values)
    {
        UnrecognizedFields.Add((key, values));
    }

    protected virtual void SerializeUnrecognizedField(string key, string[] values, string entryDelimiter, string valueDelimiter, StringBuilder builder)
    {
        builder.Append(key);

        if (values.Length > 0)
            builder.Append(valueDelimiter);

        for (int i = 0; i < values.Length - 1; i++)
        {
            builder.Append(values[i]);
            builder.Append(valueDelimiter);
        }

        if (values.Length > 0)
            builder.Append(values[^1]);

        builder.Append(entryDelimiter);
    }

    private static void CheckInit(Type containerType)
    {
        if (SerializationData.ContainsKey(containerType))
            return;

        var elementData = new Dictionary<string, FieldSerializationData>();
        SerializationData.Add(containerType, elementData);

        var properties = containerType.GetProperties().Where(property => Attribute.IsDefined(property, typeof(SaveFieldAttribute)));

        foreach (var prop in properties)
        {
            SaveFieldAttribute metadata = (SaveFieldAttribute)prop.GetCustomAttribute(typeof(SaveFieldAttribute))!;

            try
            {
                elementData.Add(metadata.Name, new FieldSerializationData
                {
                    Metadata = metadata,
                    Prop = prop,
                    Serializer = GetSerializer(prop) ?? throw new InvalidOperationException($"Serializer is missing for {prop.Name}."),
                    Deserializer = GetDeserializer(prop) ?? throw new InvalidOperationException($"Deserializer is missing for {prop.Name}."),
                });
            }
            catch (Exception e)
            {
                Logger.Warn("Failed to add element data.");
                Logger.Warn(e.ToString());
            }
        }

        var unsetOrders = elementData.Select(x => x.Value.Metadata.Order).Where(x => x == -9999).ToList();

        if (unsetOrders.Count > 0)
            Logger.Warn($"{unsetOrders.Count} Order properties for container {containerType} are unset!");

        var ordersSet = elementData.Select(x => x.Value.Metadata.Order).Where(x => x != -9999).ToList();
        var distinct = ordersSet.Distinct().ToList();

        if (ordersSet.Count != distinct.Count)
            Logger.Warn($"{ordersSet.Count - distinct.Count} Order properties for container {containerType} are duplicate!");
    }

    public string SerializeFields(string valueDelimiter, string entryDelimiter)
    {
        StringBuilder builder = new();

        foreach (var data in SerializationData[GetType()].Values.OrderBy(x => x.Metadata.Order))
        {
            var serializer = data.Serializer;

            if (serializer == null)
            {
                Logger.Warn($"{GetType()} => {data.Prop.Name} is missing a serializer.");
                continue;
            }

            serializer(this, out var keys, out var valuesArray);

            if (keys.Length != valuesArray.Length)
            {
                Logger.Warn($"{GetType()} => {data.Prop.Name} deserializer returned a differing number of keys and values.");
                continue;
            }

            foreach ((var key, var values) in keys.Zip(valuesArray))
            {
                var keyToUse = key ?? data.Metadata.Name;

                builder.Append(keyToUse);

                if (values.Length > 0)
                    builder.Append(valueDelimiter);

                for (int i = 0; i < values.Length - 1; i++)
                {
                    builder.Append(values[i]);
                    builder.Append(valueDelimiter);
                }

                if (values.Length > 0)
                    builder.Append(values[^1]);

                if (data.Metadata.TrailingValueDelimiter)
                    builder.Append(valueDelimiter);

                builder.Append(entryDelimiter);
            }
        }

        foreach ((var key, var values) in UnrecognizedFields)
        {
            SerializeUnrecognizedField(key, values, entryDelimiter, valueDelimiter, builder);
        }

        return builder.ToString();
    }

    private IEnumerable<(string Key, string[] Values)> GetFields(string data, string valueDelimiter, string entryDelimiter)
    {
        string[] entries = data.Split(entryDelimiter);

        int count = 0;

        foreach (var entry in entries.Where(string.IsNullOrEmpty))
        {
            DeserializeUnrecognizedField(entry, []);
            count++;
        }

        if (count > 0)
        {
            Logger.Info($"Add {count} empty fields in ${GetType()} as unrecognized values.");
        }

        foreach (var entry in entries.Where(x => !string.IsNullOrEmpty(x)))
        {
            string[] fields = entry.Split(valueDelimiter);

            if (fields.Length >= 2)
            {
                yield return (fields[0], fields[1..]);
            }
            else if (fields.Length == 1)
            {
                yield return (fields[0], []);
            }
            else
            {
                Logger.Error($"Failed to read an entry.");
            }
        }
    }

    private static bool IsMultiList(Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(MultiList<>);
    }

    private static bool IsMatchingMultilistField(string key, FieldSerializationData data)
    {
        if (data.Metadata.MultiListMode == MultiListMode.Prefix && key.StartsWith(data.Metadata.Name))
            return true;

        if (data.Metadata.MultiListMode == MultiListMode.Substring && key.Contains(data.Metadata.Name))
            return true;

        return false;
    }

    private FieldSerializationData? GetDataFromKey(string key, out bool requireUnique)
    {
        // Check multilists for any keys that prefix this key
        // If there is such an element, use that type
        // If there are multiple prefixes that match, use the longest
        var multilistData = SerializationData[GetType()]
            .Where(x => IsMultiList(x.Value.Prop.PropertyType))
            .OrderByDescending(x => x.Value.Metadata.Name.Length)
            .FirstOrDefault(x => IsMatchingMultilistField(key, x.Value));

        if (multilistData.Key != null)
        {
            requireUnique = false;
            return multilistData.Value;
        }
        
        // Otherwise just grab exact matches
        if (SerializationData[GetType()].TryGetValue(key, out FieldSerializationData data))
        {
            requireUnique = true;
            return data;
        }

        requireUnique = true;
        return null;
    }

    public void DeserializeFields(string serializedData, string valueDelimiter, string entryDelimiter)
    {
        var keysEncountered = new Dictionary<string, int>();

        foreach ((var key, var values) in GetFields(serializedData, valueDelimiter, entryDelimiter))
        {
            FieldSerializationData? elementData = GetDataFromKey(key, out bool requireUnique);

            if (elementData == null)
            {
                Logger.Warn($"{GetType()}: {LimitString(key)} doesn't have serialization data. Adding to unknown fields.");
                DeserializeUnrecognizedField(key, values);
                continue;
            }

            if (requireUnique && !keysEncountered.TryAdd(key, 1))
            {
                keysEncountered[key]++;
                DeserializeUnrecognizedField(key, values);
                continue;
            }

            var data = elementData.Value;
            var deserializer = data.Deserializer;
            
            if (deserializer == null)
            {
                Logger.Warn($"{GetType()} => {data.Prop.Name} does not have a deserializer. Adding to unknown fields.");
                DeserializeUnrecognizedField(key, values);
                continue;
            }

            try
            {
                deserializer(this, key, values);
            }
            catch (Exception e)
            {
                Logger.Error($"{GetType()} => {data.Prop.Name} deserialized threw an exception during call. Adding to unknown fields.");
                Logger.Error(e.ToString());
                DeserializeUnrecognizedField(key, values);
                continue;
            }
        }

        foreach (var (key, times) in keysEncountered)
        {
            if (times > 1)
                Logger.Warn($"{GetType()}: Found {times - 1} duplicate {LimitString(key)} keys. Adding to unknown fields");
        }
    }

    private static string LimitString(string input)
    {
        return input.Length <= MaxDebugChars ? input : input[..MaxDebugChars] + "...";
    }
}
