using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

[DebuggerDisplay("{SongName} | Last played in cycle {CycleLastPlayed}")]
public class SongPlayRecord : IParsable<SongPlayRecord>
{
    public string SongName { get; set; } = "";

    public int CycleLastPlayed { get; set; }

    public static SongPlayRecord Parse(string s, IFormatProvider? provider)
    {
        var record = new SongPlayRecord();

        // TODO validate that it's exactly two fields
        var data = s.Split("<dpD>");
        record.SongName = data[0];
        record.CycleLastPlayed = int.Parse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture);

        return record;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SongPlayRecord result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{SongName}<dpD>{CycleLastPlayed}";
    }
}
