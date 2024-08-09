using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.Save_Elements;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
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
        string[] coordValues = s.Split('.');

        return new()
        {
            RoomName = coordValues[0],
            X = int.Parse(coordValues[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            Y = int.Parse(coordValues[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            AbstractNode = int.Parse(coordValues[3], NumberStyles.Any, CultureInfo.InvariantCulture)
        };
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out WorldCoordinate result)
    {
        if (s == null)
        {
            result = default;
            return false;
        }

        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public override string ToString()
    {
        return $"{RoomName}.{X}.{Y}.{AbstractNode}";
    }
}
