using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using RainWorldSaveAPI.Base;

namespace RainWorldSaveAPI.SaveElements;
public class Community : IRWSerializable<Community>
{
    public Dictionary<string, float> PlayerRegionalReputation { get; } = [];

    public static Community Deserialize(string key, string[] values, SerializationContext? context)
    {
        var community = new Community();

        var pairs = values[0].Split("|");

        foreach (var pair in pairs)
        {
            // TODO Handle less than 2 elements
            var elements = pair.Split(":", 2);
            community.PlayerRegionalReputation[elements[0]] = float.Parse(elements[1], NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        return community;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            string.Join("|", PlayerRegionalReputation.Select(x => $"{x.Key}:{x.Value}"))
        ];

        return true;
    }
}

// TODO do modded creature communities exist?
// Vultu: 🤓 Ackshully the DLC is technically a mod
public class CreatureCommunities : SaveElementContainer, IRWSerializable<CreatureCommunities>
{
    public float ScavengerShynesss { get; set; } = 0f;

    public Dictionary<string, Community> Communities { get; private set; } = [];

    public static CreatureCommunities Deserialize(string key, string[] values, SerializationContext? context)
    {
        CreatureCommunities data = new CreatureCommunities();

        if (!values[0].Contains("<ccA>") && values[0].Contains("<coA>"))
        {
            foreach ((var fieldKey, var fieldValue) in SaveUtils.GetFields(values[0], "<coB>", "<coA>"))
            {
                if (fieldKey == "SCAVSHY")
                {
                    if (!float.TryParse(fieldValue, out float shyness))
                        Logger.Error($"Unable to parse \"SCAVSHY\" from value: \"{fieldValue}\"");
                    data.ScavengerShynesss = shyness;
                    continue;
                }
                else
                    data.Communities.Add(fieldKey, Community.Deserialize(fieldKey, [fieldValue], null));
            }
        }
        else
        {
            // TODO
            throw new InvalidOperationException("The save file's creature communities format is not supported yet.");
        }

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"SCAVSHY<coB>{ScavengerShynesss}<coA>" +
                string.Join("<coA>", Communities.Select(x =>
                {
                    x.Value.Serialize(out _, out var values, null);
                    return $"{x.Key}<coB>{values[0]}";
                }))
        ];

        return true;
    }
}
