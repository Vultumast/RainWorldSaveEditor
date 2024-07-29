using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Slugcat = {Slugcat} | ColorsEnabled = {ColorsEnabled} | Colors = {string.Join(\", \", ColorChoices)}")]
public class ColorChoice : IParsable<ColorChoice>
{
    public string Slugcat { get; set; } = "White";

    public bool ColorsEnabled { get; set; } = false;

    public List<string> ColorChoices { get; set; } = [];

    public static ColorChoice Parse(string s, IFormatProvider? provider)
    {
        var data = new ColorChoice();

        var parts = s.Split("<mpdB>", StringSplitOptions.RemoveEmptyEntries);

        data.Slugcat = parts[0];
        data.ColorsEnabled = parts[1] == "1";

        if (parts.Length > 2)
        {
            data.ColorChoices.AddRange(parts[2].Split("<mpdC>", StringSplitOptions.RemoveEmptyEntries));
        }

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ColorChoice result)
    {
        throw new NotImplementedException();
    }
}
