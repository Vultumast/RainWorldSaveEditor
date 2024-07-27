using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveEditor.Save;

[DebuggerDisplay("Survived = {Survived} | Travelled = {Travelled}")]
public class SessionRecord : IParsable<SessionRecord>
{
    public bool Survived { get; set; } = false;

    public bool Travelled { get; set; } = false;

    public string UnrecognizedRecords { get; set; } = "";

    public static SessionRecord Parse(string s, IFormatProvider? provider)
    {
        var record = new SessionRecord();

        record.Survived = s[0] == '1';
        record.Travelled = s[1] == '1';
        record.UnrecognizedRecords = s[2..];

        return record;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SessionRecord result)
    {
        throw new NotImplementedException();
    }
}