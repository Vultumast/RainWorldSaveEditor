using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Booleans = {ToString()}")]
public class GenericBoolArray : IRWSerializable<GenericBoolArray>
{
    public bool[] Booleans { get; set; } = [];

    public bool TryGet(int index, bool fallback = false)
    {
        if (0 <= index && index < Booleans.Length)
            return Booleans[index];

        return fallback;
    }

    public void TrySet(int index, bool value)
    {
        if (0 <= index && index < Booleans.Length)
            Booleans[index] = value;
    }

    public static GenericBoolArray Deserialize(string key, string[] values, SerializationContext? context)
    {
        var array = new GenericBoolArray
        {
            Booleans = values[0].Select(x => x == '1').ToArray()
        };

        return array;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            string.Concat(Booleans.Select(x => x ? '1' : '0'))
        ];

        return true;
    }
}
