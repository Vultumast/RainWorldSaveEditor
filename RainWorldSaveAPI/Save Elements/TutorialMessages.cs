using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

public class TutorialMessages : IRWSerializable<TutorialMessages>
{
    public List<string> Messages { get; } = [];

    public static TutorialMessages Deserialize(string key, string[] values, SerializationContext? context)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var messages = new TutorialMessages();

        messages.Messages.Clear();
        messages.Messages.AddRange(values[0].Split(","));

        return messages;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            string.Join(",", Messages)
        ];

        return true;
    }
}
