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

[DebuggerDisplay("[{SaveStateNumber} | Rain World v{GameVersion} | World v{WorldVersion}]")]
public class SaveState : SaveElementContainer
{
    public SaveState() : base()
    {

    }

    /// <summary>
    /// Location of the current shelter.
    /// </summary>
    [SaveFileElement("DENPOS")]
    public string DenPosition { get; set; } = "???";

    /// <summary>
    /// Location of the last shelter visited that is available in the base (vanilla) game.
    /// </summary>
    [SaveFileElement("LASTVDENPOS")]
    public string LastVanillaDen { get; set; } = "???";

    /// <summary>
    /// Number of cycles passed.
    /// </summary>
    [SaveFileElement("CYCLENUM")]
    public int CycleNumber { get; set; } = 0;

    /// <summary>
    /// Current number of filled food pips.
    /// </summary>
    [SaveFileElement("FOOD")]
    public int FoodCount { get; set; } = 0;

    /// <summary>
    /// Used when spawning entities. <para/> 
    /// This value is incremented then the resulting ID is assigned to each new spawned entity.
    /// </summary>
    [SaveFileElement("NEXTID")]
    public int NextIssuedId { get; set; } = 0;

    /// <summary>
    /// True if the player is affected by the neuron glow effect, false otherwise.
    /// </summary>
    [SaveFileElement("HASTHEGLOW", true)]
    public bool HasNeuronGlow { get; set; } = false;

    /// <summary>
    /// Unused. <para/>
    /// If true, the guiding overseer will not spawn for the player.
    /// </summary>
    [SaveFileElement("GUIDEOVERSEERDEAD", true)]
    public bool IsGuideOverseerDead { get; set; } = false;

    /// <summary>
    /// IDs of creatures that will respawn during the next cycle.
    /// </summary>
    [SaveFileElement("RESPAWNS", ListDelimiter = ".")]
    public List<int> CreaturesToRespawn { get; private set; } = [];

    /// <summary>
    /// IDs of creatures that are waiting to respawn. <para/>
    /// Each cycle, there is a 33% chance that they'll be added to the respawn list (50% with Hunter, Artificer and Spearmaster, 100% in Rubicon).
    /// </summary>
    [SaveFileElement("WAITRESPAWNS", ListDelimiter = ".")]
    public List<int> CreaturesWaitingToRespawn { get; private set; } = [];

    // TODO Document this
    [SaveFileElement("REGIONSTATE")]
    public RegionStateList RegionStates { get; private set; } = [];

    /// <summary>
    /// Community-related data, mostly composed of player reputation for each community in each region.
    /// </summary>
    [SaveFileElement("COMMUNITIES")]
    public CreatureCommunities Communities { get; set; } = new();

    /// <summary>
    /// Contains data that persists across player deaths.
    /// </summary>
    [SaveFileElement("DEATHPERSISTENTSAVEDATA")]
    public DeathPersistentSaveData DeathPersistentSaveData { get; private set; } = new();

    /// <summary>
    /// Contains serialized strings of swallowed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDSWALLOWED", ListDelimiter = "<svB>")]
    public List<string> UnrecognizedSwallowedItems { get; } = [];

    /// <summary>
    /// Contains serialized strings of swallowed items
    /// </summary>
    [SaveFileElement("SWALLOWEDITEMS", ListDelimiter = "<svB>")]
    public List<string> SwallowedItems { get; private set; } = [];

    /// <summary>
    /// Contains serialized strings of grabbed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDPLAYERGRASPS")]
    public List<string> UnrecognizedPlayerGrasps { get; } = [];

    /// <summary>
    /// The version of Rain World for this save.
    /// </summary>
    [SaveFileElement("VERSION")]
    public int GameVersion { get; set; } = 0;

    /// <summary>
    /// The initial version of Rain World for this save. <para/>
    /// PS: might be the same as VERSION.
    /// </summary>
    [SaveFileElement("INITVERSION")]
    public int InitialGameVersion { get; set; } = 0;

    /// <summary>
    /// The current world version for this save. <para/>
    /// Rain World will try updating old saves to the newest world version on load.
    /// </summary>
    [SaveFileElement("WORLDVERSION")]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// Random seed generated on save creation. <para/>
    /// This is mainly used to determine which pearl text will be used by Moon.
    /// </summary>
    [SaveFileElement("SEED")]
    public int Seed { get; set; } = 0;

    /// <summary>
    /// Contains data related to shelter dreams. <para/>
    /// This is missing entirely for slugcats that don't have dreams.
    /// </summary>
    [SaveFileElement("DREAMSSTATE")]
    public DreamsState? DreamsState { get; set; } = null;

    /// <summary>
    /// Stores the total number of food pips consumed during this playthough.
    /// </summary>
    [SaveFileElement("TOTFOOD")]
    public int TotalFoodEaten { get; set; } = 0;

    /// <summary>
    /// Stores the total time elapsed in seconds during this playthrough.
    /// </summary>
    [SaveFileElement("TOTTIME")]
    public int TotalTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// Stores the number of cycles played on the same world version. <para/>
    /// This is reset whenever the save world version is updated.
    /// </summary>
    [SaveFileElement("CURRVERCYCLES")]
    public int CyclesInCurrentWorldVersion { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of kills for each creature.
    /// </summary>
    [SaveFileElement("KILLS", ListDelimiter="<svC>")]
    public List<(string Thing, string Count)> Kills { get; } = [];

    /// <summary>
    /// Indicates whenever the player received extra cycles from visiting Five Pebbles.
    /// </summary>
    [SaveFileElement("REDEXTRACYCLES", true)]
    public bool HunterExtraCycles { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player just beat the game. <para/>
    /// This is used to restore the player's food pips in a similar way to Fast Travels when continuing right after a win.
    /// </summary>
    [SaveFileElement("JUSTBEATGAME", true)]
    public bool GameRecentlyBeaten { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has a Citizen Drone following it. <para/>
    /// This is used for the Artificer campaign.
    /// </summary>
    [SaveFileElement("HASROBO", true)]
    public bool HasCitizenDrone { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player is wearing Moon's cloak.
    /// </summary>
    [SaveFileElement("CLOAK", true)]
    public bool IsWearingCloak { get; set; } = false;

    /// <summary>
    /// Tracks whenever Saint has reached max Karma cap and has seen the related karma dream.
    /// </summary>
    [SaveFileElement("KARMADREAM", true)]
    public bool KarmaDream { get; set; } = false;

    /// <summary>
    /// Tracks whenever pups will be forced to spawn for the next cycle.
    /// 0 = Gourmand not beaten / no effect
    /// 1 = Max allowed number of pups guaranteed to spawn next cycle
    /// 2 = Chance based pup spawns
    /// </summary>
    [SaveFileElement("FORCEPUPS")]
    public int ForcePupsNextCycle { get; set; } = 0;

    // ObjectTrackers

    /// <summary>
    /// Saved objects and critters in the world.
    /// </summary>
    [SaveFileElement("OBJECTS")]
    public List<string> Objects { get; private set; } = [];

    /// <summary>
    /// Saved friendly creatures.
    /// </summary>
    [SaveFileElement("FRIENDS")]
    public List<string> Friends { get; private set; } = [];

    /// <summary>
    /// Tracks the list of slugcat encounters for Gourmand in the Outer Expanse.
    /// </summary>
    [SaveFileElement("OEENCOUNTERS")]
    public List<string> OuterExpanseEncounters { get; private set; } = [];

    /// <summary>
    /// The internal name of the slugcat campaign.
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
