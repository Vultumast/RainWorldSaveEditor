using RainWorldSaveAPI.SaveElements;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI.Save_Elements;

public class LineageCounter : IParsable<LineageCounter>
{
    public WorldCoordinate Den { get; set; } = new();

    /// <summary>
    /// Doesn't seem to be used by the game.
    /// </summary>
    public string ConflictNumber { get; set; } = "0";

    public int Counter { get; set; }

    public static LineageCounter Parse(string s, IFormatProvider? provider)
    {
        var data = new LineageCounter();

        var parts = s.Split(':');
        var denParts = parts[0].Split(';');

        data.Den = WorldCoordinate.Parse(denParts[0], null);
        data.ConflictNumber = denParts.Length >= 2 ? denParts[1] : "0";
        data.Counter = int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LineageCounter result)
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
        if (ConflictNumber != "0")
        {
            return $"{Den};{ConflictNumber}:{Counter}";
        }
        else
        {
            return $"{Den}:{Counter}";
        }
    }
}
