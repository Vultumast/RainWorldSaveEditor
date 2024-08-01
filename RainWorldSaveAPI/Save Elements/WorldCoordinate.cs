using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

// TODO: Check if this is the only format for world coordinates that is used
[DebuggerDisplay("Room = {RoomName} | Pos = {X}, {Y} | AbstractNode = {AbstractNode}")]
public class WorldCoordinate : IParsable<WorldCoordinate>
{
    public string RoomName { get; set; } = "???";

    public int X { get; set; }

    public int Y { get; set; }

    public int AbstractNode { get; set; }

    public static WorldCoordinate Parse(string s, IFormatProvider? provider)
    {
        // TODO handle less / more than 4 values
        string[] values = s.Split('.');

        return new()
        {
            RoomName = values[0],
            X = int.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            Y = int.Parse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            AbstractNode = int.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture)
        };
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out WorldCoordinate result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{RoomName}.{X}.{Y}.{AbstractNode}";
    }
}
