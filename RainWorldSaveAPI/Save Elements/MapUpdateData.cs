using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.Save_Elements;

[SerializeRaw(":")]
public class MapUpdateData : IParsable<MapUpdateData>
{
    public string Key { get; set; } = "";

    public string Region { get; set; } = "";

    public long MapLastUpdated { get; set; } = 0;

    public static MapUpdateData Parse(string s, IFormatProvider? provider)
    {
        var mapData = new MapUpdateData();

        var parts = s.Split(":", 2);

        mapData.Key = parts[0];

        var valueParts = parts[1].Split("<progDivB>");

        mapData.Region = valueParts[0];
        mapData.MapLastUpdated = long.Parse(valueParts[1], NumberStyles.Any, CultureInfo.InvariantCulture);

        return mapData;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MapUpdateData result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{Key}:{Region}<progDivB>{MapLastUpdated}";
    }
}
