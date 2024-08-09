using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public class Stick : IParsable<Stick>
{
    public int Room { get; set; } = 0;
    public string StickType { get; set; } = "";

    public string EntityIDA { get; set; } = "";
    public string EntityIDB { get; set; } = "";

    public List<string> State { get; set; } = [];

    public static Stick Parse(string s, IFormatProvider? provider)
    {
        var data = new Stick();

        var parts = s.Split("<stkA>");

        data.Room = int.Parse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.StickType = parts[1];
        data.EntityIDA = parts[2];
        data.EntityIDB = parts[3];

        data.State = new(parts.Length >= 5 ? parts[4..] : []);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Stick result)
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
        return $"{Room}<stkA>" +
            $"{StickType}<stkA>" +
            $"{EntityIDA}<stkA>" +
            $"{EntityIDB}" +
            $"{string.Concat(State.Select(x => $"<stkA>{x}"))}";
    }
}
