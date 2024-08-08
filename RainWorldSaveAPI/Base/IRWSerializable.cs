using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI.Base;

public class SerializationContext(SaveFieldAttribute? metadata, PropertyInfo? prop)
{
    public SaveFieldAttribute? Metadata { get; init; } = metadata;
    public PropertyInfo? Prop { get; init; } = prop;
}

public interface IRWSerializable<T> where T : IRWSerializable<T>
{
    public static abstract T Deserialize(string key, string[] values, SerializationContext? context);
    public bool Serialize(out string? key, out string[] values, SerializationContext? context);
}
