using RainWorldSaveAPI.SaveElements;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI.Save_Elements;

public class SwarmRoomCounter : IParsable<SwarmRoomCounter>
{
    public string Room { get; set; } = "";
    public int Counter { get; set; } = 0;

    public static SwarmRoomCounter Parse(string s, IFormatProvider? provider)
    {
        var data = new SwarmRoomCounter();

        var parts = s.Split(':');

        data.Room = parts[0];
        data.Counter = int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SwarmRoomCounter result)
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
        return $"{Room}:{Counter}";
    }
}
