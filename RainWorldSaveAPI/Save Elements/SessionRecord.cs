using RainWorldSaveAPI.Base;
using System.Diagnostics;

namespace RainWorldSaveAPI.SaveElements;

[DebuggerDisplay("Survived = {Survived} | Travelled = {Travelled}")]
public class SessionRecord : IRWSerializable<SessionRecord>
{
    public bool Survived { get; set; } = false;

    public bool Travelled { get; set; } = false;

    public string UnrecognizedRecords { get; set; } = "";

    public static SessionRecord Deserialize(string key, string[] values, SerializationContext? context)
    {
        var record = new SessionRecord
        {
            Survived = values[0][0] == '1',
            Travelled = values[0][1] == '1',
            UnrecognizedRecords = values[0].Length > 2 ? values[0][2..] : ""
        };

        return record;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{(Survived ? '1' : '0')}{(Travelled ? '1' : '0')}{UnrecognizedRecords}"
        ];

        return true;
    }
}