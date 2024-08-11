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

    /// <summary>
    /// Set by the game if the world coordinate's room wasn't recognized by the game. <para/>
    /// Seems to be mostly a save debug feature, since the room is always rechecked when loading.
    /// </summary>
    public bool WasMarkedAsInvalid { get; set; } = false; // Leave this on false in the editor

    public static WorldCoordinate Parse(string s, IFormatProvider? provider)
    {
        // TODO handle less / more than 5 values
        Span<string> coordValues = s.Split('.');
        bool wasMarkedAsInvalid = false;

        if (coordValues.Length == 5 && coordValues[0] == "INV")
        {
            coordValues = coordValues[1..]; // INV usually marks that the room is unknown for whatever reason
            wasMarkedAsInvalid = true;
        }

        return new()
        {
            RoomName = coordValues[0],
            X = int.Parse(coordValues[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            Y = int.Parse(coordValues[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            AbstractNode = int.Parse(coordValues[3], NumberStyles.Any, CultureInfo.InvariantCulture),
            WasMarkedAsInvalid = wasMarkedAsInvalid
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
        return $"{(WasMarkedAsInvalid ? "INV." : "")}{RoomName}.{X}.{Y}.{AbstractNode}";
    }
}
