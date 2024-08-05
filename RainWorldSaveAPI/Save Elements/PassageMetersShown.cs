using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

public class PassageMetersShown : IRWSerializable<PassageMetersShown>
{
    public List<string> Passages { get; } = [];

    public static PassageMetersShown Deserialize(string key, string[] values, SerializationContext? context)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var messages = new PassageMetersShown();

        messages.Passages.Clear();
        messages.Passages.AddRange(values[0].Split(","));

        return messages;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            string.Join(",", Passages)
        ];

        return true;
    }
}
