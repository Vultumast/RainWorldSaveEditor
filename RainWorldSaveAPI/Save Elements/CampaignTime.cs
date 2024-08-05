using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Globalization;
using RainWorldSaveAPI.Base;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Slugcat = {Slugcat} | FreeTime = {UndeterminedFreeTime} / {CompletedFreeTime} / {LostFreeTime} | FixedTime = {UndeterminedFixedTime} / {CompletedFixedTime} / {LostFixedTime} |")]
public class CampaignTime : IRWSerializable<CampaignTime>
{
    public string Slugcat { get; set; } = "White";

    public double UndeterminedFreeTime { get; set; }

    public double CompletedFreeTime { get; set; }

    public double LostFreeTime { get; set; }

    public double UndeterminedFixedTime { get; set; }

    public double CompletedFixedTime { get; set; }

    public double LostFixedTime { get; set; }

    public static CampaignTime Deserialize(string key, string[] values, SerializationContext? context)
    {
        return new CampaignTime
        {
            Slugcat = values[0],
            UndeterminedFreeTime = double.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture),
            CompletedFreeTime = double.Parse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture),
            LostFreeTime = double.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture),
            UndeterminedFixedTime = double.Parse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture),
            CompletedFixedTime = double.Parse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture),
            LostFixedTime = double.Parse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture)
        };
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            Slugcat,
            $"{UndeterminedFreeTime}",
            $"{CompletedFreeTime}",
            $"{LostFreeTime}",
            $"{UndeterminedFixedTime}",
            $"{CompletedFixedTime}",
            $"{LostFixedTime}"
        ];

        return true;
    }
}
