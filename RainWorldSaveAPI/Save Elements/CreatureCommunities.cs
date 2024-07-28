using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using RainWorldSaveAPI.Base;

namespace RainWorldSaveAPI.SaveElements;
public class Community : IParsable<Community>
{
    public Dictionary<string, float> PlayerRegionalReputation { get; } = [];

    public static Community Parse(string s, IFormatProvider? provider)
    {
        var community = new Community();

        var pairs = s.Split("|");

        foreach (var pair in pairs)
        {
            // TODO Handle less than 2 elements
            var elements = pair.Split(":", 2);
            community.PlayerRegionalReputation[elements[0]] = float.Parse(elements[1], NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        return community;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Community result)
    {
        throw new NotImplementedException();
    }
}

// TODO do modded creature communities exist?
// Vultu: 🤓 Ackshully the DLC is technically a mod
public class CreatureCommunities : SaveElementContainer, IParsable<CreatureCommunities>
{
    public float ScavengerShynesss { get; set; } = 0f;

    public Dictionary<string, Community> Communities { get; private set; } = [];



    public static CreatureCommunities Parse(string s, IFormatProvider? provider)
    {
        // This has two ways of parsing, maybe due to backwards compatibility?

        CreatureCommunities data = new CreatureCommunities();

        if (!s.Contains("<ccA>") && s.Contains("<coA>"))
        {
            foreach ((var key, var value) in SaveUtils.GetFields(s, "<coB>", "<coA>"))
            {
                if (key == "SCAVSHY")
                {
                    float shyness = 0;
                    if (!float.TryParse(value, out shyness))
                        Logger.Error($"Unable to parse \"SCAVSHY\" from value: \"{value}\"");
                    data.ScavengerShynesss = shyness;
                    continue;
                }
                else
                    data.Communities.Add(key, Community.Parse(value, provider));
                
                // ParseField(data, key, value);
            }
        }
        else
        {
            throw new InvalidOperationException("The save file's creature communities format is not supported yet.");
        }

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out CreatureCommunities result)
    {
        // Vultu: probably make this better
        result = Parse(s, provider);
        return true;
    }
}
