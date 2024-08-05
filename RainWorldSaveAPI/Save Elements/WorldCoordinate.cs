using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

// TODO: Check if this is the only format for world coordinates that is used
[DebuggerDisplay("Room = {RoomName} | Pos = {X}, {Y} | AbstractNode = {AbstractNode}")]
public class WorldCoordinate : IRWSerializable<WorldCoordinate>
{
    public string RoomName { get; set; } = "???";

    public int X { get; set; }

    public int Y { get; set; }

    public int AbstractNode { get; set; }

    public static WorldCoordinate Deserialize(string key, string[] values, SerializationContext? context)
    {
        // TODO handle less / more than 4 values
        string[] coordValues = values[0].Split('.');

        return new()
        {
            RoomName = coordValues[0],
            X = int.Parse(coordValues[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            Y = int.Parse(coordValues[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            AbstractNode = int.Parse(coordValues[3], NumberStyles.Any, CultureInfo.InvariantCulture)
        };
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{RoomName}.{X}.{Y}.{AbstractNode}"
        ];

        return true;
    }
}
