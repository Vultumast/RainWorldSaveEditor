using System.Reflection;

namespace RainWorldSaveAPI.Base;

public static class Serializers
{
    private static readonly NullabilityInfoContext NullInfo = new();

    public delegate void PropDeserializer(SaveElementContainer container, string key, string[] values);
    public delegate void PropSerializer(SaveElementContainer container, out string?[] keys, out string[][] values);

    public static PropDeserializer? GenerateDeserializer(PropertyInfo prop)
    {
        if (IsParsable(prop.PropertyType))
        {
            var method = typeof(Serializers).GetMethod(nameof(ParsableDeserializer))!.MakeGenericMethod(prop.PropertyType);
            return (PropDeserializer)method.Invoke(null, [prop])!;
        }

        if (IsRWSerializable(prop.PropertyType))
        {
            var method = typeof(Serializers).GetMethod(nameof(RWDeserializer))!.MakeGenericMethod(prop.PropertyType);
            return (PropDeserializer)method.Invoke(null, [prop])!;
        }
        
        if (IsMultiList(prop.PropertyType))
        {
            var elementType = prop.PropertyType.GetGenericArguments()[0];

            if (IsParsable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(ParsableMultiListDeserializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropDeserializer)method.Invoke(null, [prop])!;
            }
            else if (IsRWSerializable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(SerializableMultiListDeserializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropDeserializer)method.Invoke(null, [prop])!;
            }
            else
            {
                Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create deserializer.");
                return null;
            }
        }

        if (IsList(prop.PropertyType))
        {
            var elementType = prop.PropertyType.GetGenericArguments()[0];

            if (IsParsable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(ParsableListDeserializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropDeserializer)method.Invoke(null, [prop])!;
            }
            else if (IsRWSerializable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(SerializableListDeserializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropDeserializer)method.Invoke(null, [prop])!;
            }
            else
            {
                Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create deserializer.");
                return null;
            }
        }

        Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create deserializer.");
        return null;
    }

    public static PropSerializer? GenerateSerializer(PropertyInfo prop)
    {
        if (IsParsable(prop.PropertyType))
        {
            var method = typeof(Serializers).GetMethod(nameof(ParsableSerializer))!.MakeGenericMethod(prop.PropertyType);
            return (PropSerializer)method.Invoke(null, [prop])!;
        }

        if (IsRWSerializable(prop.PropertyType))
        {
            var method = typeof(Serializers).GetMethod(nameof(RWSerializer))!.MakeGenericMethod(prop.PropertyType);
            return (PropSerializer)method.Invoke(null, [prop])!;
        }

        if (IsMultiList(prop.PropertyType))
        {
            var elementType = prop.PropertyType.GetGenericArguments()[0];

            if (IsParsable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(ParsableMultiListSerializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropSerializer)method.Invoke(null, [prop])!;
            }
            else if (IsRWSerializable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(SerializableMultiListSerializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropSerializer)method.Invoke(null, [prop])!;
            }
            else
            {
                Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create serializer.");
                return null;
            }
        }

        if (IsList(prop.PropertyType))
        {
            var elementType = prop.PropertyType.GetGenericArguments()[0];

            if (IsParsable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(ParsableListSerializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropSerializer)method.Invoke(null, [prop])!;
            }
            else if (IsRWSerializable(elementType))
            {
                var method = typeof(Serializers).GetMethod(nameof(SerializableListSerializer))!.MakeGenericMethod(prop.PropertyType, elementType);
                return (PropSerializer)method.Invoke(null, [prop])!;
            }
            else
            {
                Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create serializer.");
                return null;
            }
        }

        Logger.Warn($"{prop.DeclaringType} => {prop.Name}: Failed to create serializer.");
        return null;
    }

    public static PropDeserializer SerializableMultiListDeserializer<T, U>(PropertyInfo prop)
        where T : MultiList<U>
        where U : IRWSerializable<U>
    {
        if (!IsMultiList(prop.PropertyType) || !IsRWSerializable(prop.PropertyType.GetGenericArguments()[0]))
            throw new InvalidOperationException("Expected multilist with serializable type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        return (SaveElementContainer container, string key, string[] values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            list.Add(U.Deserialize(key, values, null));
        };
    }

    public static PropDeserializer ParsableMultiListDeserializer<T, U>(PropertyInfo prop)
        where T : MultiList<U>
        where U : IParsable<U>
    {
        if (!IsMultiList(prop.PropertyType) || !IsParsable(prop.PropertyType.GetGenericArguments()[0]))
            throw new InvalidOperationException("Expected multilist with parsable type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        return (SaveElementContainer container, string key, string[] values) =>
        {
            if (values.Length != 1)
                throw new ArgumentException($"{prop.DeclaringType} => {prop.Name}: Expected {typeof(T)} type to have exactly one value.");

            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            list.Add(U.Parse(values[0], null));
        };
    }

    public static PropSerializer SerializableMultiListSerializer<T, U>(PropertyInfo prop)
        where T : MultiList<U>
        where U : IRWSerializable<U>
    {
        if (!IsMultiList(prop.PropertyType) || !IsRWSerializable(prop.PropertyType.GetGenericArguments()[0]))
            throw new InvalidOperationException("Expected multilist with serializable type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            keys = new string?[list.Count];
            values = new string[list.Count][];

            int count = 0;

            foreach (var element in list)
            {
                if (!element.Serialize(out keys[count], out values[count], new(prop.GetCustomAttribute<SaveFileElement>(), null)))
                    throw new InvalidOperationException("Failed to serialize.");

                count++;
            }
        };
    }

    public static PropSerializer ParsableMultiListSerializer<T, U>(PropertyInfo prop)
        where T : MultiList<U>
        where U : IParsable<U>
    {
        if (!IsMultiList(prop.PropertyType) || !IsParsable(prop.PropertyType.GetGenericArguments()[0]))
            throw new InvalidOperationException("Expected multilist with parsable type.");

        var toStringMethod = typeof(U).GetMethod(nameof(ToString), []);

        if (toStringMethod == null || toStringMethod.DeclaringType == typeof(object))
            throw new InvalidOperationException("Parsable either has a missing ToString method or is using the default object.ToString implementation.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            keys = new string?[list.Count];
            values = new string[list.Count][];

            int count = 0;

            foreach (var element in list)
            {
                var value = element.ToString() ?? throw new InvalidOperationException("ToString returned null");

                keys[count] = null;
                values[count] = [value];
                count++;
            }
        };
    }

    public static PropDeserializer ParsableDeserializer<T>(PropertyInfo prop)
        where T : IParsable<T>
    {
        if (typeof(T) == typeof(bool))
            return BoolDeserializer(prop);

        else if (!typeof(T).IsAssignableFrom(prop.PropertyType))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var setter = prop.GetSetMethod(true) ?? throw new InvalidOperationException("Expected property to have a setter.");

        return (container, key, values) =>
        {
            if (values.Length != 1)
                throw new ArgumentException($"{prop.DeclaringType} => {prop.Name}: Expected {typeof(T)} type to have exactly one value.");

            setter.Invoke(container, [T.Parse(values[0], null)]);
        };
    }

    public static PropSerializer ParsableSerializer<T>(PropertyInfo prop)
        where T : IParsable<T>
    {
        if (typeof(T) == typeof(bool))
            return BoolSerializer(prop);

        else if (!typeof(T).IsAssignableFrom(prop.PropertyType))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var toStringMethod = typeof(T).GetMethod(nameof(ToString), []);

        if (toStringMethod == null || toStringMethod.DeclaringType == typeof(object))
            throw new InvalidOperationException("Parsable either has a missing ToString method or is using the default object.ToString implementation.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");
        var nullableAllowed = NullInfo.Create(prop).ReadState == NullabilityState.Nullable;

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            object? value = getter.Invoke(container, []);

            if (value == null)
            {
                if (!nullableAllowed)
                    throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name}: Expected {typeof(T)} that is not nullable to not return null.");

                keys = [];
                values = [];
            }
            else
            {
                keys = [null];
                values = [[value.ToString() ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name}: ToString() for parsable returned null.")]];
            }
        };
    }

    public static PropDeserializer RWDeserializer<T>(PropertyInfo prop)
        where T : IRWSerializable<T>
    {
        if (!typeof(T).IsAssignableFrom(prop.PropertyType))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var setter = prop.GetSetMethod(true) ?? throw new InvalidOperationException("Expected property to have a setter.");

        return (container, key, values) =>
        {
            setter.Invoke(container, [T.Deserialize(key, values, new(prop.GetCustomAttribute<SaveFileElement>(), prop))]);
        };
    }

    public static PropSerializer RWSerializer<T>(PropertyInfo prop)
        where T : IRWSerializable<T>
    {
        if (!typeof(T).IsAssignableFrom(prop.PropertyType))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");
        var nullableAllowed = NullInfo.Create(prop).ReadState == NullabilityState.Nullable;

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            object? value = getter.Invoke(container, []);

            if (value == null)
            {
                if (!nullableAllowed)
                    throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name}: Expected {typeof(T)} that is not nullable to not return null.");

                keys = [];
                values = [];
            }
            else
            {
                if (value is not IRWSerializable<T> serializable)
                    throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name}: Expected {typeof(T)} to be returned, instead got {value.GetType()}");

                keys = [null];
                values = [[]];

                if (!serializable.Serialize(out keys[0], out values[0], new(prop.GetCustomAttribute<SaveFileElement>(), prop)))
                {
                    keys = [];
                    values = [];
                }
            }
        };
    }

    public static PropDeserializer BoolDeserializer(PropertyInfo prop)
    {
        if (prop.PropertyType != typeof(bool))
            throw new InvalidOperationException("Expected bool type.");

        var setter = prop.GetSetMethod(true) ?? throw new InvalidOperationException("Expected property to have a setter.");

        return (container, key, values) =>
        {
            if (values.Length > 0)
                throw new ArgumentException($"{prop.DeclaringType} => {prop.Name}: Expected boolean type to have no values.");

            setter.Invoke(container, [true]);
        };
    }

    public static PropSerializer BoolSerializer(PropertyInfo prop)
    {
        if (prop.PropertyType != typeof(bool))
            throw new InvalidOperationException("Expected bool type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            bool shouldSerialize = (bool)getter.Invoke(container, [])!;

            if (shouldSerialize)
            {
                keys = [null];
                values = [[]];
            }
            else
            {
                keys = [];
                values = [];
            }
        };
    }

    public static PropDeserializer SerializableListDeserializer<T, U>(PropertyInfo prop)
        where T : List<U>
        where U : IRWSerializable<U>
    {
        if (prop.PropertyType != typeof(T))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        var listDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.ListDelimiter ?? throw new ArgumentException("Expected list delimiter to be defined for serializable list.");
        
        return (container, key, values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            if (values.Length != 1)
                throw new ArgumentException($"{prop.DeclaringType} => {prop.Name}: expected exactly one value for serializable list of type {typeof(T)}.");

            foreach (var value in values[0].Split(listDelimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(U.Deserialize(key, [value], null));
            }
        };
    }

    public static PropSerializer SerializableListSerializer<T, U>(PropertyInfo prop)
        where T : List<U>
        where U : IRWSerializable<U>
    {
        if (prop.PropertyType != typeof(T))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        var listDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.ListDelimiter ?? throw new ArgumentException("Expected list delimiter to be defined for serializable list.");
        var trailingListDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.TrailingListDelimiter ?? throw new ArgumentException("Expected trailing delimiter to be defined for serializable list.");
        var serializeListIfEmpty = prop.GetCustomAttribute<SaveFileElement>()?.SerializeIfEmpty ?? throw new ArgumentException("Expected attribute for list.");

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            if (list.Count == 0 && !serializeListIfEmpty)
            {
                keys = [];
                values = [];
                return;
            }

            string combined = "";
            int count = 0;

            foreach (var element in list)
            {
                element.Serialize(out _, out var elementValues, null);

                if (elementValues.Length == 0)
                    throw new InvalidOperationException($"{typeof(T)} encountered a {typeof(U)} element that serialized to zero values!");

                if (elementValues.Length > 2)
                    throw new InvalidOperationException($"{typeof(T)} encountered a {typeof(U)} element that serialized to more than one value!");

                count++;
                combined += elementValues[0] + (trailingListDelimiter || count < list.Count ? listDelimiter : "");
            }

            keys = [null];
            values = [[combined]];
        };
    }

    public static PropDeserializer ParsableListDeserializer<T, U>(PropertyInfo prop)
        where T : List<U>
        where U : IParsable<U>
    {
        if (prop.PropertyType != typeof(T))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        var listDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.ListDelimiter ?? throw new ArgumentException("Expected list delimiter to be defined for serializable list.");
        
        return (container, key, values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            if (values.Length != 1)
                throw new ArgumentException($"{prop.DeclaringType} => {prop.Name}: expected exactly one value for serializable list of type {typeof(T)}.");

            foreach (var value in values[0].Split(listDelimiter, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(U.Parse(value, null));
            }
        };
    }

    public static PropSerializer ParsableListSerializer<T, U>(PropertyInfo prop)
        where T : List<U>
        where U : IParsable<U>
    {
        if (prop.PropertyType != typeof(T))
            throw new InvalidOperationException($"Expected {typeof(T)} type.");

        var toStringMethod = typeof(U).GetMethod(nameof(ToString), []);

        if (toStringMethod == null || toStringMethod.DeclaringType == typeof(object))
            throw new InvalidOperationException("Parsable either has a missing ToString method or is using the default object.ToString implementation.");

        var getter = prop.GetGetMethod(true) ?? throw new InvalidOperationException("Expected property to have a getter.");

        var listDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.ListDelimiter ?? throw new ArgumentException("Expected list delimiter to be defined for serializable list.");
        var trailingListDelimiter = prop.GetCustomAttribute<SaveFileElement>()?.TrailingListDelimiter ?? throw new ArgumentException("Expected trailing delimiter to be defined for serializable list.");
        var serializeListIfEmpty = prop.GetCustomAttribute<SaveFileElement>()?.SerializeIfEmpty ?? throw new ArgumentException("Expected attribute for list.");

        return (SaveElementContainer container, out string?[] keys, out string[][] values) =>
        {
            var list = getter.Invoke(container, []) as T ?? throw new InvalidOperationException($"{prop.DeclaringType} => {prop.Name} expected to return {typeof(T)} instead of null or other type.");

            if (list.Count == 0 && !serializeListIfEmpty)
            {
                keys = [];
                values = [];
                return;
            }

            string combined = "";
            int count = 0;

            foreach (var element in list)
            {
                var value = element.ToString();

                count++;
                combined += value + (trailingListDelimiter || count < list.Count ? listDelimiter : "");
            }

            keys = [null];
            values = [[combined]];
        };
    }

    private static bool IsParsable(Type t)
    {
        return t.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IParsable<>).Name)?.GetGenericTypeDefinition() == typeof(IParsable<>);
    }

    private static bool IsRWSerializable(Type t)
    {
        return t.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IRWSerializable<>).Name)?.GetGenericTypeDefinition() == typeof(IRWSerializable<>);
    }

    private static bool IsMultiList(Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(MultiList<>);
    }

    private static bool IsList(Type t)
    {
        return t.GetInterfaces().FirstOrDefault(x => x.Name == typeof(IList<>).Name)?.GetGenericTypeDefinition() == typeof(IList<>);
    }
}
