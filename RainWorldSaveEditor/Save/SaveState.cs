using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Save;

public class SaveState
{
    public Dictionary<string, string> UnrecognizedFields { get; } = [];

    /// <summary>
    /// DENPOS
    /// </summary>
    public string DenPosition { get; set; } = "???";

    /// <summary>
    /// LASTVDENPOS
    /// </summary>
    public string LastVanillaDen { get; set; } = "???";

    /// <summary>
    /// CYCLENUM
    /// </summary>
    public int CycleNumber { get; set; } = 0;

    /// <summary>
    /// FOOD
    /// </summary>
    public int FoodCount { get; set; } = 0;

    /// <summary>
    /// NEXTID
    /// </summary>
    public int NextIssuedId { get; set; } = 0;

    /// <summary>
    /// HASGLOW (valueless)
    /// </summary>
    public bool HasNeuronGlow { get; set; } = false;

    /// <summary>
    /// GUIDEOVERSEERDEAD (valueless)
    /// </summary>
    public bool IsGuideOvereerDead { get; set; } = false;

    /// <summary>
    /// RESPAWNS
    /// </summary>
    public List<int> CreaturesToRespawn { get; } = [];

    /// <summary>
    /// WAITRESPAWNS
    /// </summary>
    public List<int> CreaturesWaitingToRespawn { get; } = [];

    public void Read(string data)
    {
        foreach ((var key, var value) in SaveUtils.GetFields(data, "<svB>", "<svA>"))
        {
            ParseField(key, value);
        }
    }

    private void ParseField(string key, string value)
    {
        switch (key)
        {
            case "DENPOS":
                DenPosition = value;
                break;
            case "LASTVDENPOS":
                LastVanillaDen = value;
                break;
            case "CYCLENUM":
                CycleNumber = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture); // TODO Handle parse fail?
                break;
            case "FOOD":
                FoodCount = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "NEXTID":
                NextIssuedId = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "HASTHEGLOW":
                HasNeuronGlow = true;
                break;
            case "GUIDEOVERSEERDEAD":
                IsGuideOvereerDead = true;
                break;
            case "RESPAWNS":
                // TODO Error handling
                CreaturesToRespawn.Clear();
                CreaturesToRespawn.AddRange(value.Split('.').Where(x => x != "").Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)));
                break;
            case "WAITRESPAWNS":
                // TODO Error handling
                CreaturesWaitingToRespawn.Clear();
                CreaturesWaitingToRespawn.AddRange(value.Split('.').Where(x => x != "").Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)));
                break;
            case "REGIONSTATE":
            case "COMMUNITIES":
            case "MISCWORLDSAVEDATA":
            case "DEATHPERSISTENTSAVEDATA":
            case "SWALLOWEDITEMS":
            case "UNRECOGNIZEDSWALLOWED":
            case "PLAYERGRASPS":
            case "UNRECOGNIZEDPLAYERGRASPS":
            case "VERSION":
            case "INITVERSION":
            case "WORLDVERSION":
            case "SEED":
            case "DREAMSSTATE":
            case "TOTFOOD":
            case "TOTTIME":
            case "CURRVERCYCLES":
            case "KILLS":
            case "REDEXTRACYCLES":
            case "JUSTBEATGAME":
            case "HASROBO":
            case "CLOAK":
            case "KARMADREAM":
            case "FORCEPUPS":
            case "OBJECTTRACKERS":
            case "OBJECTS":
            case "FRIENDS":
            case "OEENCOUNTERS":
            case "SAV STATE NUMBER":
                // Unused
                break;
            default:
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
        }
    }

    public string Write()
    {
        throw new NotImplementedException();
    }
}
