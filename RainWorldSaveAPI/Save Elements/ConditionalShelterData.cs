using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Shelter = {ShelterName} | Slugcats = {string.Join(\", \", SlugcatFoundList)}")]
public class ConditionalShelterData : IRWSerializable<ConditionalShelterData>
{
    public string ShelterName { get; set; } = "";

    public List<string> SlugcatFoundList { get; set; } = [];

    public static ConditionalShelterData Deserialize(string key, string[] values, SerializationContext? context)
    {
        var data = new ConditionalShelterData();

        var parts = values[0].Split(" : ", StringSplitOptions.RemoveEmptyEntries);

        data.ShelterName = parts[0];
        data.SlugcatFoundList.AddRange(parts[1..]);

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{ShelterName} : {string.Join(" : ", SlugcatFoundList)} : "
        ];

        return true;
    }
}
