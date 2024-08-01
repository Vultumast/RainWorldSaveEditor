using System.Reflection;

namespace RainWorldSaveAPI.Base;

public abstract class SaveElementContainer
{
    private struct ElementData
    {
        public SaveFileElement SaveFileElement;
        public PropertyInfo PropertyInfo;
    }

    private const int MaxDebugChars = 50;

    public SaveElementContainer()
    {
        CheckForDictionaryInit();
    }

    private void CheckForDictionaryInit()
    {
        if (Elements.ContainsKey(GetType()))
            return;

        var elementData = new Dictionary<string, ElementData>();

        var properties = GetType().GetProperties().Where(property => Attribute.IsDefined(property, typeof(SaveFileElement)));

        foreach (var prop in properties)
        {
            SaveFileElement saveFileElement = (SaveFileElement)prop.GetCustomAttribute(typeof(SaveFileElement))!;

            elementData.Add(saveFileElement.Name, new ElementData
            {
                SaveFileElement = saveFileElement,
                PropertyInfo = prop
            });
        }

        Elements.Add(GetType(), elementData);
    }

    private static Dictionary<Type, Dictionary<string, ElementData>> Elements { get; } = [];

    public Dictionary<string, string> UnrecognizedFields { get; protected set; } = [ ];

    private static readonly MethodInfo SetScalarPropertyMethod = typeof(SaveElementContainer).GetMethod(nameof(SetScalarProperty), BindingFlags.NonPublic | BindingFlags.Instance)!;
    private static readonly MethodInfo SetListPropertyMethod = typeof(SaveElementContainer).GetMethod(nameof(SetListProperty), BindingFlags.NonPublic | BindingFlags.Instance)!;


    /// <summary>
    /// Sets the value for a scalar property. I.E. A property that is not a collection
    /// </summary>
    /// <param name="container">The SaveElementContainer who is calling</param>
    /// <param name="propertyInfo">The information of the property</param>
    /// <param name="elementInfo">The SaveFileElement information for the property</param>
    /// <param name="value">The value to set the property too</param>
    /// <returns>True on success, false otherwise</returns>
    private bool SetScalarProperty<T>(PropertyInfo propertyInfo, SaveFileElement elementInfo, string value)
        where T : IParsable<T>
    {
        if (typeof(T) == typeof(bool) && elementInfo.ValueOptional)
        {
            var setMethod = propertyInfo.GetSetMethod(true);
            if (setMethod is null)
            {
                Logger.Warn($"Set Method was null for \"{propertyInfo.Name}\" with container: \"{GetType()}\"");
                return false;
            }
            setMethod.Invoke(this, [ true ]);
        }

        if (!elementInfo.ValueOptional && value == string.Empty)
        {
            Logger.Error($"\"{elementInfo.Name}\" is NOT marked as ValueOptional, but no value was provided! Tell Mario or Vultu!");
            return false;
        }

        if (value != string.Empty)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.GetSetMethod()!.Invoke(this, [value]);
            }
            else
            {
                var propertyBinding = propertyInfo.GetValue(this);

                var data = T.Parse(value, null);

                var setMethod = propertyInfo.GetSetMethod(true);
                if (setMethod is null)
                {
                    Logger.Error($"Set Method was null for \"{propertyInfo.Name}\" with container: \"{GetType()}\"");
                    return false;
                }
                setMethod.Invoke(this, [data]);
            }
        }
        else if (!elementInfo.ValueOptional)
        {
            Logger.Error($"UNABLE TO SET: \"{propertyInfo.Name}\"! Tell Mario or Vultu!");
            return false;
        }

        return true;
    }

    private bool SetListProperty<T, U>(PropertyInfo propertyInfo, SaveFileElement elementInfo, string value)
        where T : ICollection<U>
        where U : IParsable<U>
    {
        var propertyBinding = (T)propertyInfo.GetValue(this)!;

        if (elementInfo.IsRepeatableKey != RepeatMode.None)
        {
            propertyBinding.Add(U.Parse(value, null));
        }
        else
        {
            propertyBinding.Clear();

            foreach (var element in value.Split(elementInfo.ListDelimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                propertyBinding.Add(U.Parse(element, null));
            }
        }

        return true;
    }

    private void HandleUnrecognizedField(string key, string value)
    {
        if (!UnrecognizedFields.TryAdd(key, value))
            Logger.Warn($"Unable to set \"{key}\" because it was already present!");

        // TODO Remove this later
        Logger.Debug($"Unknown field: {key} => {LimitString(value)}");
    }

    public static void ParseField(SaveElementContainer container, string key, string value) => container.ParseField(key, value);

    private void ParseField(string key, string value)
    {
        ElementData? elementData = GetRelevantElementInfo(key);

        if (elementData != null)
        {
            (var elementInfo, var propertyInfo) = (elementData.Value.SaveFileElement, elementData.Value.PropertyInfo);

            var collectionInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>)).FirstOrDefault();
            var parsableInterface = propertyInfo.PropertyType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IParsable<>)).FirstOrDefault();

            if (parsableInterface is not null)
            {
                var actualValueToUse = value;

                var rawAttrib = propertyInfo.PropertyType.GetCustomAttribute<SerializeRawAttribute>();

                if (rawAttrib != null)
                {
                    actualValueToUse = key + rawAttrib.KeyValueDelimiter + value;
                }

                if ((bool)SetScalarPropertyMethod.MakeGenericMethod(parsableInterface.GetGenericArguments()[0]).Invoke(this, [propertyInfo, elementInfo, actualValueToUse])!)
                    return;
            }
            else if (collectionInterface is not null)
            {
                var actualValueToUse = value;
                var elementType = propertyInfo.PropertyType.GetGenericArguments()[0];

                var rawAttrib = elementType.GetCustomAttribute<SerializeRawAttribute>();

                if (rawAttrib != null)
                {
                    actualValueToUse = key + rawAttrib.KeyValueDelimiter + value;
                }

                if (elementInfo.ListDelimiter is not null && elementInfo.IsRepeatableKey != RepeatMode.None)
                    Logger.Debug($"{key} => {LimitString(value)}, \"{propertyInfo.PropertyType}\" is a collection that has both a delimiter and is marked as repeatable! Tell Mario or Vultu!");

                if (elementInfo.ListDelimiter is null && elementInfo.IsRepeatableKey == RepeatMode.None)
                    Logger.Debug($"{key} => {LimitString(value)}, \"{propertyInfo.PropertyType}\" is a collection that doesn't have a delimiter and is not marked as repeatable! Tell Mario or Vultu!");

                else if ((bool)SetListPropertyMethod.MakeGenericMethod(collectionInterface, elementType).Invoke(this, [propertyInfo, elementInfo, actualValueToUse])!)
                    return;
            }
            else
                Logger.Debug($"{key} => {LimitString(value)}, \"{propertyInfo.PropertyType}\" does not derive from IParsable! Tell Mario or Vultu!");

            HandleUnrecognizedField(key, value);
        }
        else
            Logger.Warn($"Unknown Key: \"{key}\" => {LimitString(value)}");
    }

    private ElementData? GetRelevantElementInfo(string key)
    {
        var prefixedInfo = Elements[GetType()].Values.FirstOrDefault(x => key.StartsWith(x.SaveFileElement.Name) && x.SaveFileElement.IsRepeatableKey == RepeatMode.Prefix);

        if (prefixedInfo.SaveFileElement != null && prefixedInfo.PropertyInfo != null)
            return prefixedInfo;

        if (Elements[GetType()].TryGetValue(key, out ElementData info))
            return info;

        return null;
    }

    private static string LimitString(string input)
    {
        return input.Length <= MaxDebugChars ? input : input[..MaxDebugChars] + "...";
    }
}
