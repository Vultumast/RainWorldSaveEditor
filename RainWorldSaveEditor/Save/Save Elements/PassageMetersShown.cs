using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveEditor.Save;

public class PassageMetersShown : IParsable<PassageMetersShown>
{
    public List<string> Passages { get; } = [];

    public static PassageMetersShown Parse(string s, IFormatProvider? provider)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var messages = new PassageMetersShown();

        messages.Passages.Clear();
        messages.Passages.AddRange(s.Split(","));

        return messages;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PassageMetersShown result)
    {
        throw new NotImplementedException();
    }
}
