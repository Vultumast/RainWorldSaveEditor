using RainWorldSaveAPI.Base;

namespace RainWorldSaveAPI.Save_Elements;

public class MapData : IRWSerializable<MapData>
{
    public string Key { get; set; } = "";

    public string Region { get; set; } = "";

    public byte[] MapDataPNG { get; set; } = [];

    public static MapData Deserialize(string key, string[] values, SerializationContext? context)
    {
        var mapData = new MapData
        {
            Key = key,
            Region = values[0],
            MapDataPNG = Convert.FromBase64String(values[1])
        };

        return mapData;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = Key;
        values = [
            Region,
            Convert.ToBase64String(MapDataPNG)
        ];

        return true;
    }
}
