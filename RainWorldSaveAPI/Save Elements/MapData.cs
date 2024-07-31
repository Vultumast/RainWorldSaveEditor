using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.Save_Elements;

[SerializeRaw(":")]
public class MapData : IParsable<MapData>
{
    public string Data { get; set; } = "";

    public static MapData Parse(string s, IFormatProvider? provider)
    {
        var mapData = new MapData();

        mapData.Data = s;

        return mapData;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MapData result)
    {
        throw new NotImplementedException();
    }
}
