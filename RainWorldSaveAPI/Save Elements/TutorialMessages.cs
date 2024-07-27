using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

public class TutorialMessages : IParsable<TutorialMessages>
{
    public List<string> Messages { get; } = [];

    public static TutorialMessages Parse(string s, IFormatProvider? provider)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var messages = new TutorialMessages();

        messages.Messages.Clear();
        messages.Messages.AddRange(s.Split(","));

        return messages;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TutorialMessages result)
    {
        throw new NotImplementedException();
    }
}
