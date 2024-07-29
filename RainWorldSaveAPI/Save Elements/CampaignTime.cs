using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Slugcat = {Slugcat} | ColorsEnabled = {ColorsEnabled} | Colors = {string.Join(\", \", ColorChoices)}")]
public class CampaignTime : IParsable<CampaignTime>
{
    public string Slugcat { get; set; } = "White";

    public double UndeterminedFreeTime { get; set; }

    public double CompletedFreeTime { get; set; }

    public double LostFreeTime { get; set; }

    public double UndeterminedFixedTime { get; set; }

    public double CompletedFixedTime { get; set; }

    public double LostFixedTime { get; set; }

    public static CampaignTime Parse(string s, IFormatProvider? provider)
    {
        var data = new CampaignTime();

        var parts = s.Split("<mpdB>", StringSplitOptions.RemoveEmptyEntries);

        data.Slugcat = parts[0];
        data.UndeterminedFreeTime = double.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.CompletedFreeTime = double.Parse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.LostFreeTime = double.Parse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.UndeterminedFixedTime = double.Parse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.CompletedFixedTime = double.Parse(parts[5], NumberStyles.Any, CultureInfo.InvariantCulture);
        data.LostFixedTime = double.Parse(parts[6], NumberStyles.Any, CultureInfo.InvariantCulture);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CampaignTime result)
    {
        throw new NotImplementedException();
    }
}
