using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.Save_Elements;

[SerializeRaw(":")]
public class MapUpdateData : IParsable<MapUpdateData>
{
    public string Data { get; set; } = "";

    public static MapUpdateData Parse(string s, IFormatProvider? provider)
    {
        var mapData = new MapUpdateData();

        mapData.Data = s;

        return mapData;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MapUpdateData result)
    {
        throw new NotImplementedException();
    }
}
