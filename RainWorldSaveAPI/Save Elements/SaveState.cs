using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;

namespace RainWorldSaveAPI;

[DebuggerDisplay("{SaveStateNumber} | Game version {GameVersion} | World version {WorldVersion}")]
public class SaveState : SaveElementContainer, IRWSerializable<SaveState>
{
    /// <summary>
    /// The internal name of the slugcat campaign.
    /// </summary>
    [SaveField(0, "SAV STATE NUMBER")]
    public string SaveStateNumber { get; set; } = "???";

    /// <summary>
    /// Random seed generated on save creation. <para/>
    /// This is mainly used to determine which pearl text will be used by Moon.
    /// </summary>
    [SaveField(1, "SEED")]
    public int Seed { get; set; } = 0;

    /// <summary>
    /// The version of Rain World for this save.
    /// </summary>
    [SaveField(2, "VERSION")]
    public int GameVersion { get; set; } = 0;

    /// <summary>
    /// The initial version of Rain World for this save. <para/>
    /// PS: might be the same as VERSION.
    /// </summary>
    [SaveField(3, "INITVERSION")]
    public int InitialGameVersion { get; set; } = 0;

    /// <summary>
    /// The current world version for this save. <para/>
    /// Rain World will try updating old saves to the newest world version on load.
    /// </summary>
    [SaveField(4, "WORLDVERSION")]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// Location of the current shelter.
    /// </summary>
    [SaveField(5, "DENPOS")]
    public string DenPosition { get; set; } = DefaultDen;

    /// <summary>
    /// Location of the last shelter visited that is available in the base (vanilla) game.
    /// </summary>
    [SaveField(6, "LASTVDENPOS")]
    public string LastVanillaDen { get; set; } = DefaultDen;

    /// <summary>
    /// Number of cycles passed.
    /// </summary>
    [SaveField(7, "CYCLENUM")]
    public int CycleNumber { get; set; } = 0;

    /// <summary>
    /// Current number of filled food pips.
    /// </summary>
    [SaveField(8, "FOOD")]
    public int FoodCount { get; set; } = 0;

    /// <summary>
    /// Used when spawning entities. <para/> 
    /// This value is incremented then the resulting ID is assigned to each new spawned entity.
    /// </summary>
    [SaveField(9, "NEXTID")]
    public int NextIssuedId { get; set; } = 0;

    /// <summary>
    /// True if the player is affected by the neuron glow effect, false otherwise.
    /// </summary>
    [SaveField(10, "HASTHEGLOW")]
    public bool HasNeuronGlow { get; set; } = false;

    /// <summary>
    /// Unused. <para/>
    /// If true, the guiding overseer will not spawn for the player.
    /// </summary>
    [SaveField(11, "GUIDEOVERSEERDEAD")]
    public bool IsGuideOverseerDead { get; set; } = false;

    /// <summary>
    /// IDs of creatures that will respawn during the next cycle.
    /// </summary>
    [SaveField(12, "RESPAWNS", ListDelimiter = ".")]
    public List<int> CreaturesToRespawn { get; private set; } = [];

    /// <summary>
    /// IDs of creatures that are waiting to respawn. <para/>
    /// Each cycle, there is a 33% chance that they'll be added to the respawn list (50% with Hunter, Artificer and Spearmaster, 100% in Rubicon).
    /// </summary>
    [SaveField(13, "WAITRESPAWNS", ListDelimiter = ".")]
    public List<int> CreaturesWaitingToRespawn { get; private set; } = [];

    /// <summary>
    /// Community-related data, mostly composed of player reputation for each community in each region.
    /// </summary>
    [SaveField(14, "COMMUNITIES")]
    public CreatureCommunities Communities { get; set; } = new();

    /// <summary>
    /// Tracks the state of each region, including objects and entities present.
    /// </summary>
    [SaveField(15, "REGIONSTATE")]
    public MultiList<RegionState> RegionStates { get; private set; } = [];

    // TODO: Implement item deserialization
    /// <summary>
    /// Contains serialized strings of swallowed items
    /// </summary>
    [SaveField(16, "SWALLOWEDITEMS", ListDelimiter = "", SerializeIfEmpty = false)]
    public List<AbstractObjectOrCreature> SwallowedItems { get; set; } = new();

    /// <summary>
    /// Contains serialized strings of swallowed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveField(17, "UNRECOGNIZEDSWALLOWED")]
    public RawValues UnrecognizedSwallowedItems { get; set; } = new();

    // TODO: Implement grab deserialization
    /// <summary>
    /// Contains serialized strings of grabbed items and creatures <para/>
    /// </summary>
    [SaveField(18, "PLAYERGRASPS", ListDelimiter = "")]
    public List<AbstractObjectOrCreature> PlayerGrasps { get; set; } = new();

    /// <summary>
    /// Contains serialized strings of grabbed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveField(19, "UNRECOGNIZEDPLAYERGRASPS")]
    public RawValues UnrecognizedPlayerGrasps { get; set; } = new();

    /// <summary>
    /// Contains data that persists across player deaths.
    /// </summary>
    [SaveField(20, "DEATHPERSISTENTSAVEDATA")]
    public DeathPersistentSaveData DeathPersistentSaveData { get; private set; } = new();

    /// <summary>
    /// Contains miscellaneous data mostly related to Iterator / Downpour events.
    /// </summary>
    [SaveField(21, "MISCWORLDSAVEDATA")]
    public MiscWorldSaveData MiscWorldSaveData { get; private set; } = new();

    /// <summary>
    /// Contains data related to shelter dreams. <para/>
    /// This is missing entirely for slugcats that don't have dreams.
    /// </summary>
    [SaveField(22, "DREAMSSTATE")]
    public DreamsState? DreamsState { get; set; } = null;

    /// <summary>
    /// Stores the total number of food pips consumed during this playthough.
    /// </summary>
    [SaveField(23, "TOTFOOD")]
    public int TotalFoodEaten { get; set; } = 0;

    /// <summary>
    /// Stores the total time elapsed in seconds during this playthrough.
    /// </summary>
    [SaveField(24, "TOTTIME")]
    public int TotalTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// Stores the number of cycles played on the same world version. <para/>
    /// This is reset whenever the save world version is updated.
    /// </summary>
    [SaveField(25, "CURRVERCYCLES")]
    public int CyclesInCurrentWorldVersion { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of kills for each creature.
    /// </summary>
    [SaveField(26, "KILLS", ListDelimiter = "<svC>")]
    public List<CreatureKillData> Kills { get; } = [];

    /// <summary>
    /// Indicates whenever the player received extra cycles from visiting Five Pebbles.
    /// </summary>
    [SaveField(27, "REDEXTRACYCLES")]
    public bool HunterExtraCycles { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player just beat the game. <para/>
    /// This is used to restore the player's food pips in a similar way to Fast Travels when continuing right after a win.
    /// </summary>
    [SaveField(28, "JUSTBEATGAME")]
    public bool GameRecentlyBeaten { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has a Citizen Drone following it. <para/>
    /// This is used for the Artificer campaign.
    /// </summary>
    [SaveField(29, "HASROBO")]
    public bool HasCitizenDrone { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player is wearing Moon's cloak.
    /// </summary>
    [SaveField(30, "CLOAK")]
    public bool IsWearingCloak { get; set; } = false;

    /// <summary>
    /// Tracks whenever Saint has reached max Karma cap and has seen the related karma dream.
    /// </summary>
    [SaveField(31, "KARMADREAM")]
    public bool KarmaDream { get; set; } = false;

    /// <summary>
    /// Tracks whenever pups will be forced to spawn for the next cycle.
    /// 0 = Gourmand not beaten / no effect
    /// 1 = Max allowed number of pups guaranteed to spawn next cycle
    /// 2 = Chance based pup spawns
    /// </summary>
    [SaveField(32, "FORCEPUPS")]
    public int ForcePupsNextCycle { get; set; } = 0;

    // TODO Implement detailed object tracker deserialization
    /// <summary>
    /// List used for tracking persistent objects, and resspawning them if they are lost. <para/>
    /// This works only if the Remix option is set.
    /// </summary>
    [SaveField(33, "OBJECTTRACKERS", ListDelimiter = "<svC>")]
    public List<string> ObjectTrackers { get; private set; } = [];

    /// <summary>
    /// Saved objects and critters in the world.
    /// </summary>
    [SaveField(34, "OBJECTS", ListDelimiter = "<svC>", SerializeIfEmpty = false)]
    public List<string> Objects { get; private set; } = [];

    /// <summary>
    /// Saved friendly creatures.
    /// </summary>
    [SaveField(35, "FRIENDS", ListDelimiter = "<svC>")]
    public List<string> Friends { get; private set; } = [];

    /// <summary>
    /// Tracks the list of slugcat encounters for Gourmand in the Outer Expanse.
    /// </summary>
    [SaveField(36, "OEENCOUNTERS", ListDelimiter = "<svC>")]
    public List<string> OuterExpanseEncounters { get; private set; } = [];

    public static SaveState Deserialize(string key, string[] values, SerializationContext? context)
    {
        SaveState data = new();

        data.DeserializeFields(values[0], "<svB>", "<svA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<svB>", "<svA>")
        ];

        return true;
    }

    protected override void DeserializeUnrecognizedField(string key, string[] values)
    {
        if (key.Trim() != "")
        {
            if (values.Length >= 1)
            {
                UnrecognizedFields.Add((key, [values[0]]));
            }
            else
            {
                UnrecognizedFields.Add((key, []));
            }
        }
    }

    /// <summary>
    /// This is the Outskirts shelter that's closest to Industrial Complex's Karma Gate. <para/>
    /// </summary>
    private const string DefaultDen = "SU_S04";
}
