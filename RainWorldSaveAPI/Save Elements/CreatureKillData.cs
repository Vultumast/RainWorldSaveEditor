using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public class CreatureKillData : IParsable<CreatureKillData>
{
    public string Creature { get; set; } = "";

    public int Kills { get; set; } = 0;

    public static CreatureKillData Parse(string s, IFormatProvider? provider)
    {
        var data = new CreatureKillData();

        var parts = s.Split("<svD>");

        data.Creature = parts[0];
        data.Kills = int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CreatureKillData result)
    {
        throw new NotImplementedException();
    }
}
