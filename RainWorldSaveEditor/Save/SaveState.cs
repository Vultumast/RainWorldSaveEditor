using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Save;

public class SaveState : SaveElementContainer
{
    public SaveState() : base()
    {

    }

    /// <summary>
    /// DENPOS
    /// </summary>
    [SaveFileElement("DENPOS")]
    public string DenPosition { get; set; } = "???";

    /// <summary>
    /// LASTVDENPOS
    /// </summary>
    [SaveFileElement("LASTVDENPOS")]
    public string LastVanillaDen { get; set; } = "???";

    /// <summary>
    /// CYCLENUM
    /// </summary>
    [SaveFileElement("CYCLENUM")]
    public int CycleNumber { get; set; } = 0;

    /// <summary>
    /// FOOD
    /// </summary>
    [SaveFileElement("FOOD")]
    public int FoodCount { get; set; } = 0;

    /// <summary>
    /// NEXTID
    /// </summary>
    [SaveFileElement("NEXTID")]
    public int NextIssuedId { get; set; } = 0;

    /// <summary>
    /// HASTHEGLOW (valueless)
    /// </summary>
    [SaveFileElement("HASTHEGLOW", true)]
    public bool HasNeuronGlow { get; set; } = false;

    /// <summary>
    /// GUIDEOVERSEERDEAD (valueless)
    /// </summary>
    [SaveFileElement("GUIDEOVERSEERDEAD", true)]
    public bool IsGuideOverseerDead { get; set; } = false;

    /// <summary>
    /// RESPAWNS
    /// </summary>
    [SaveFileElement("RESPAWNS")]
    public IntList CreaturesToRespawn { get; private set; } = [];

    /// <summary>
    /// WAITRESPAWNS
    /// </summary>
    [SaveFileElement("WAITRESPAWNS")]
    public IntList CreaturesWaitingToRespawn { get; private set; } = [];

    /// <summary>
    /// REGIONSTATE
    /// </summary>
    [SaveFileElement("REGIONSTATE")]
    public RegionStateList RegionStates { get; private set; } = [];

    [SaveFileElement("COMMUNITIES")]
    public CreatureCommunities Communities { get; set; } = new();

    /// <summary>
    /// DEATHPERSISTENTSAVEDATA
    /// </summary>
    [SaveFileElement("DEATHPERSISTENTSAVEDATA")]
    public DeathPersistentSaveData DeathPersistentSaveData { get; private set; } = new();

    /// <summary>
    /// UNRECOGNIZEDSWALLOWED
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDSWALLOWED")]
    public List<string> UnrecognizedSwallowedItems { get; } = [];

    /// <summary>
    /// UNRECOGNIZEDPLAYERGRASPS
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDPLAYERGRASPS")]
    public List<string> UnrecognizedPlayerGrasps { get; } = [];

    /// <summary>
    /// VERSION
    /// </summary>
    [SaveFileElement("VERSION")]
    public int GameVersion { get; set; } = 0;

    /// <summary>
    /// INITVERSION
    /// </summary>
    [SaveFileElement("INITVERSION")]
    public int InitialGameVersion { get; set; } = 0;

    /// <summary>
    /// WORLDVERSION
    /// </summary>
    [SaveFileElement("WORLDVERSION")]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// SEED
    /// </summary>
    [SaveFileElement("SEED")]
    public int Seed { get; set; } = 0;

    /// <summary>
    /// DREAMSSTATE
    /// May be missing for some scugs
    /// </summary>
    [SaveFileElement("DREAMSSTATE")]
    public DreamsState? DreamsState { get; set; }

    /// <summary>
    /// TOTFOOD
    /// Refers to number of full pips.
    /// </summary>
    [SaveFileElement("TOTFOOD")]
    public int TotalFoodEaten { get; set; } = 0;

    /// <summary>
    /// TOTTIME
    /// Stored as seconds.
    /// </summary>
    [SaveFileElement("TOTTIME")]
    public int TotalTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// CURRVERCYCLES
    /// </summary>
    [SaveFileElement("CURRVERCYCLES")]
    public int CyclesInCurrentWorldVersion { get; set; } = 0;

    /// <summary>
    /// KILLS
    /// </summary>
    [SaveFileElement("KILLS")]
    public List<(string Thing, string Count)> Kills { get; } = [];

    /// <summary>
    /// REDEXTRACYCLES (valueless)
    /// Indicates whenever the player received extra cycles from visiting Five Pebbles.
    /// </summary>
    [SaveFileElement("REDEXTRACYCLES", true)]
    public bool HunterExtraCycles { get; set; } = false;

    /// <summary>
    /// JUSTBEATGAME (valueless)
    /// </summary>
    [SaveFileElement("JUSTBEATGAME", true)]
    public bool GameRecentlyBeaten { get; set; } = false;

    /// <summary>
    /// HASROBO (valueless)
    /// </summary>
    [SaveFileElement("HASROBO", true)]
    public bool HasCitizenDrone { get; set; } = false;

    /// <summary>
    /// CLOAK (valueless)
    /// </summary>
    [SaveFileElement("CLOAK", true)]
    public bool IsWearingCloak { get; set; } = false;

    /// <summary>
    /// KARMADREAM (valueless)
    /// </summary>
    [SaveFileElement("KARMADREAM", true)]
    public bool KarmaDream { get; set; } = false;

    /// <summary>
    /// FORCEPUPS
    /// 0 = Gourmand not beaten / no effect
    /// 1 = Max allowed number of pups guaranteed to spawn next cycle
    /// 2 = Chance based pup spawns
    /// </summary>
    [SaveFileElement("FORCEPUPS")]
    public int ForcePupsNextCycle { get; set; } = 0;

    // ObjectTrackers

    /// <summary>
    /// FRIENDS
    /// </summary>
    [SaveFileElement("OBJECTS")]
    public List<string> Objects { get; private set; } = [];

    /// <summary>
    /// FRIENDS
    /// </summary>
    [SaveFileElement("FRIENDS")]
    public List<string> Friends { get; private set; } = [];

    /// <summary>
    /// OEENCOUNTERS
    /// </summary>
    [SaveFileElement("OEENCOUNTERS")]
    public List<string> OuterExpanseEncounters { get; private set; } = [];

    /// <summary>
    /// SAV STATE NUMBER
    /// </summary>
    [SaveFileElement("SAV STATE NUMBER")]
    public string SaveStateNumber { get; set; } = "???";


    public void Read(string data)
    {
        foreach ((var key, var value) in SaveUtils.GetFields(data, "<svB>", "<svA>"))
        {
            if (key == "UNRECOGNIZEDPLAYERGRASPS")
            {
                Console.WriteLine("wawa");
            }
            ParseField(this, key, value);
        }
    }

    /*
    private void ParseField(string key, string value)
    {
        // TODO Error handling for Parse functions
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
                IsGuideOverseerDead = true;
                break;
            case "RESPAWNS":
                CreaturesToRespawn.Clear();
                CreaturesToRespawn.AddRange(value.Split('.').Where(x => x != "").Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)));
                break;
            case "WAITRESPAWNS":
                CreaturesWaitingToRespawn.Clear();
                CreaturesWaitingToRespawn.AddRange(value.Split('.').Where(x => x != "").Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)));
                break;
            case "REGIONSTATE":
                var regions = value.Split("<rgB>");

                // This may have invalid / modded / unrecognized regions
                foreach (var region in regions)
                {
                    var state = new RegionState();
                    state.Read(region);
                    RegionStates.Add(state);
                }

                break;
            case "COMMUNITIES":
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
            case "MISCWORLDSAVEDATA":
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
            case "DEATHPERSISTENTSAVEDATA":
                DeathPersistentSaveData.Read(value);
                break;
            case "SWALLOWEDITEMS":
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
            case "UNRECOGNIZEDSWALLOWED":
                UnrecognizedSwallowedItems.Clear();
                UnrecognizedSwallowedItems.AddRange(value.Split("<svB>", StringSplitOptions.RemoveEmptyEntries));
                break;
            case "PLAYERGRASPS":
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
            case "UNRECOGNIZEDPLAYERGRASPS":
                UnrecognizedPlayerGrasps.Clear();
                UnrecognizedPlayerGrasps.AddRange(value.Split("<svB>", StringSplitOptions.RemoveEmptyEntries));
                break;
            case "VERSION":
                GameVersion = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "INITVERSION":
                InitialGameVersion = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "WORLDVERSION":
                WorldVersion = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "SEED":
                Seed = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "DREAMSSTATE":
                DreamsState = new DreamsState();
                DreamsState.Read(value);
                break;
            case "TOTFOOD":
                TotalFoodEaten = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "TOTTIME":
                TotalTimeInSeconds = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "CURRVERCYCLES":
                CyclesInCurrentWorldVersion = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "KILLS":
                Kills.Clear();
                var pairs = value.Split("<svC>", StringSplitOptions.RemoveEmptyEntries);

                foreach (var pair in pairs)
                {
                    var fields = pair.Split("<svD>", 2);
                    Kills.Add((fields[0], fields[1]));
                }

                break;
            case "REDEXTRACYCLES":
                HunterExtraCycles = true;
                break;
            case "JUSTBEATGAME":
                GameRecentlyBeaten = true;
                break;
            case "HASROBO":
                HasCitizenDrone = true;
                break;
            case "CLOAK":
                IsWearingCloak = true;
                break;
            case "KARMADREAM":
                KarmaDream = true;
                break;
            case "FORCEPUPS":
                ForcePupsNextCycle = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "OBJECTTRACKERS":
                // TODO Implement remaining strings
                UnrecognizedFields[key] = value;
                break;
            case "OBJECTS":
                Objects.Clear();
                Objects.AddRange(value.Split("<svC>").Where(x => x != ""));
                break;
            case "FRIENDS":
                // TODO Parse friend data
                Friends.Clear();
                Friends.AddRange(value.Split("<svC>").Where(x => x != ""));
                break;
            case "OEENCOUNTERS":
                OuterExpanseEncounters.Clear();
                OuterExpanseEncounters.AddRange(value.Split("<svC>").Where(x => x != ""));
                break;
            case "SAV STATE NUMBER":
                SaveStateNumber = value;
                break;
            default:
                // Fallback for unrecognized / invalid / modded fields
                UnrecognizedFields[key] = value;
                break;
        }
    }
    */

    public string Write()
    {
        throw new NotImplementedException();
    }
}
