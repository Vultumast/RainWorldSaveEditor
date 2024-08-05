using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

    public static ConsumedItem Deserialize(string key, string[] values, SerializationContext? context)
    {
        Span<string> fields = values[0].Split(".", StringSplitOptions.RemoveEmptyEntries);

        if (fields.Length == 4 && fields[0] == "INV")
            fields = fields[1..]; // INV usually marks that the room is unknown for whatever reason

        var item = new ConsumedItem
        {
            Room = fields[0],
            PlacedObjectIndex = int.Parse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            WaitCycles = int.Parse(fields[2], NumberStyles.Any, CultureInfo.InvariantCulture)
        };

        return item;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{Room}.{PlacedObjectIndex}.{WaitCycles}"
        ];

        return true;
    }
}
