using RainWorldSaveAPI.Base;

namespace RainWorldSaveAPI.Save_Elements;

public class IntSerializeIfNotZero : IRWSerializable<IntSerializeIfNotZero>
{
    public int Value { get; set; } = 0;

    public static IntSerializeIfNotZero Deserialize(string key, string[] values, SerializationContext? context)
    {
        return new()
        {
            Value = int.Parse(values[0])
        };
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (Value > 0)
        {
            key = null;
            values = [Value.ToString()];
            return true;
        }
        else
        {
            key = null;
            values = [];
            return false;
        }
    }
}
