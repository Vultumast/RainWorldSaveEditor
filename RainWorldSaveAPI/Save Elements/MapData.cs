using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.Save_Elements;

[SerializeRaw(":")]
public class MapData : IParsable<MapData>
{
    public string Key { get; set; } = "";

    public string Region { get; set; } = "";

    public byte[] MapDataPNG { get; set; } = [];

    public static MapData Parse(string s, IFormatProvider? provider)
    {
        var mapData = new MapData();

        var parts = s.Split(":", 2);

        mapData.Key = parts[0];

        var valueParts = parts[1].Split("<progDivB>");

        mapData.Region = valueParts[0];
        mapData.MapDataPNG = Convert.FromBase64String(valueParts[1]);

        return mapData;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MapData result)
    {
        throw new NotImplementedException();
    }
}
