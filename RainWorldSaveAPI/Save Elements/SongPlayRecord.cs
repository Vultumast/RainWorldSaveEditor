using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

[DebuggerDisplay("{SongName} | Last played in cycle {CycleLastPlayed}")]
public class SongPlayRecord : IRWSerializable<SongPlayRecord>
{
    public string SongName { get; set; } = "";

    public int CycleLastPlayed { get; set; }

    public static SongPlayRecord Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = values[0].Split("<dpD>");

        // TODO validate that it's exactly two fields
        var record = new SongPlayRecord
        {
            SongName = data[0],
            CycleLastPlayed = int.Parse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture)
        };

        return record;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{SongName}<dpD>{CycleLastPlayed}"
        ];

        return true;
    }
}
