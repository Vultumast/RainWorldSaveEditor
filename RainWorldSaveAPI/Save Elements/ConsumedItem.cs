using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

[DebuggerDisplay("Room = {Room} | PlacedObjectIndex = {PlacedObjectIndex} | WaitCycles = {WaitCycles}")]
public class ConsumedItem : IRWSerializable<ConsumedItem>
{
    /// <summary>
    /// The room you consumed the object
    /// </summary>
    public string Room { get; set; } = "";
    /// <summary>
    /// The internal index of the object you consumed
    /// </summary>
    public int PlacedObjectIndex { get; set; } = 0;

    /// <summary>
    /// How many cycles before the item respawns?
    /// </summary>
    public int WaitCycles { get; set; } = 0;

    /// <summary>
    /// Set by the game if the consumed item's room wasn't recognized by the game. <para/>
    /// Seems to be mostly a save debug feature, since the room is always rechecked when loading.
    /// </summary>
    public bool WasMarkedAsInvalid { get; set; } = false; // Leave this on false in the editor

    public static ConsumedItem Deserialize(string key, string[] values, SerializationContext? context)
    {
        Span<string> fields = values[0].Split(".", StringSplitOptions.RemoveEmptyEntries);
        bool wasMarkedAsInvalid = false;

        if (fields.Length == 4 && fields[0] == "INV")
        {
            fields = fields[1..]; // INV usually marks that the room is unknown for whatever reason
            wasMarkedAsInvalid = true;
        }

        var item = new ConsumedItem
        {
            Room = fields[0],
            PlacedObjectIndex = int.Parse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            WaitCycles = int.Parse(fields[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            WasMarkedAsInvalid = wasMarkedAsInvalid
        };

        return item;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{(WasMarkedAsInvalid ? "INV." : "")}{Room}.{PlacedObjectIndex}.{WaitCycles}"
        ];

        return true;
    }
}
