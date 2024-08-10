using RainWorldSaveAPI.Base;
using System.Text;
using System.Text.Json;

namespace RainWorldSaveAPI;

// All of the data is supposed to be contained within the key
public class SlugBaseData : IRWSerializable<SlugBaseData>
{
    public string FieldKey { get; set; } = "";
    public string FieldValueRaw { get; set; } = "";
    public object? FieldValueDeserialized { get; set; } = null;

    public static SlugBaseData Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new SlugBaseData();

        var parts = key.Split("_SlugBaseSaveData_", 2);
        data.FieldKey = parts[0];
        data.FieldValueRaw = parts[1];

        try
        {
            var decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(data.FieldValueRaw));
            var deserializedData = JsonSerializer.Deserialize<JsonElement>(decodedData);

            data.FieldValueDeserialized = deserializedData;
        }
        catch (Exception e)
        {
            Logger.Warn($"Failed to deserialize slugbase data: {parts[0]}");
            Logger.Warn($"Exception message: {e}");
        }

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (FieldValueDeserialized == null)
        {
            // Fallback to using the raw value
            key = $"{FieldKey}_SlugBaseSaveData_{FieldValueRaw}";
            values = [];
        }
        else
        {
            var serializedData = JsonSerializer.Serialize(FieldValueDeserialized);
            var encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedData));

            key = $"{FieldKey}_SlugBaseSaveData_{encodedData}";
            values = [];
        }

        return true;
    }
}
