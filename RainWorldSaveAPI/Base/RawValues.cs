namespace RainWorldSaveAPI.Base;

public class RawValues : IRWSerializable<RawValues>
{
    public string[] Values { get; set; } = [];

    public static RawValues Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new RawValues
        {
            Values = [.. values]
        };

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (context?.Metadata == null)
            throw new InvalidOperationException("RawValues requires context metadata.");

        key = null;
        values = [..Values];
        return context.Metadata.SerializeIfEmpty || values.Length > 0;
    }
}
