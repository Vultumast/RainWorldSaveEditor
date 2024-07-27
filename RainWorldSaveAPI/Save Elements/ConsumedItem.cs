using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI.SaveElements;

[DebuggerDisplay("Room = {Room} | PlacedObjectIndex = {PlacedObjectIndex} | WaitCycles = {WaitCycles}")]
public class ConsumedItem : IParsable<ConsumedItem>
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

    public static ConsumedItem Parse(string s, IFormatProvider? provider)
    {
        var item = new ConsumedItem();

        Span<string> fields = s.Split(".", StringSplitOptions.RemoveEmptyEntries);

        if (fields.Length == 4 && fields[0] == "INV")
            fields = fields[1..]; // Effectively skips over that field?

        item.Room = fields[0];
        item.PlacedObjectIndex = int.Parse(fields[1], NumberStyles.Any, CultureInfo.InvariantCulture);
        item.WaitCycles = int.Parse(fields[2], NumberStyles.Any, CultureInfo.InvariantCulture);

        return item;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ConsumedItem result)
    {
        throw new NotImplementedException();
    }
}
