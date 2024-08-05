using RainWorldSaveAPI.Base;
using System.Globalization;

namespace RainWorldSaveAPI.Save_Elements;

public class MapUpdateData : IRWSerializable<MapUpdateData>
{
    public string Key { get; set; } = "";

    public string Region { get; set; } = "";

    public long MapLastUpdated { get; set; } = 0;

    public static MapUpdateData Deserialize(string key, string[] values, SerializationContext? context)
    {
        var mapData = new MapUpdateData
        {
            Key = key,
            Region = values[0],
            MapLastUpdated = long.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture)
        };

        return mapData;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = Key;
        values = [
            Region,
            MapLastUpdated.ToString()
        ];

        return true;
    }
}
