using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;

namespace RainWorldSaveAPI;

[DebuggerDisplay("{SaveStateNumber} | Game version {GameVersion} | World version {WorldVersion}")]
public class SaveState : SaveElementContainer, IRWSerializable<SaveState>
{
    public SaveState() : base()
    {

    }

    /// <summary>
    /// Location of the current shelter.
    /// </summary>
    [SaveFileElement("DENPOS", Order = 5)]
    public string DenPosition { get; set; } = "???";

    /// <summary>
    /// Location of the last shelter visited that is available in the base (vanilla) game.
    /// </summary>
    [SaveFileElement("LASTVDENPOS", Order = 6)]
    public string LastVanillaDen { get; set; } = "???";

    /// <summary>
    /// Number of cycles passed.
    /// </summary>
    [SaveFileElement("CYCLENUM", Order = 7)]
    public int CycleNumber { get; set; } = 0;

    /// <summary>
    /// Current number of filled food pips.
    /// </summary>
    [SaveFileElement("FOOD", Order = 8)]
    public int FoodCount { get; set; } = 0;

    /// <summary>
    /// Used when spawning entities. <para/> 
    /// This value is incremented then the resulting ID is assigned to each new spawned entity.
    /// </summary>
    [SaveFileElement("NEXTID", Order = 9)]
    public int NextIssuedId { get; set; } = 0;

    /// <summary>
    /// True if the player is affected by the neuron glow effect, false otherwise.
    /// </summary>
    [SaveFileElement("HASTHEGLOW", true, Order = 10)]
    public bool HasNeuronGlow { get; set; } = false;

    /// <summary>
    /// Unused. <para/>
    /// If true, the guiding overseer will not spawn for the player.
    /// </summary>
    [SaveFileElement("GUIDEOVERSEERDEAD", true, Order = 11)]
    public bool IsGuideOverseerDead { get; set; } = false;

    /// <summary>
    /// IDs of creatures that will respawn during the next cycle.
    /// </summary>
    [SaveFileElement("RESPAWNS", ListDelimiter = ".", Order = 12)]
    public List<int> CreaturesToRespawn { get; private set; } = [];

    /// <summary>
    /// IDs of creatures that are waiting to respawn. <para/>
    /// Each cycle, there is a 33% chance that they'll be added to the respawn list (50% with Hunter, Artificer and Spearmaster, 100% in Rubicon).
    /// </summary>
    [SaveFileElement("WAITRESPAWNS", ListDelimiter = ".", Order = 13)]
    public List<int> CreaturesWaitingToRespawn { get; private set; } = [];

    // TODO Document this
    [SaveFileElement("REGIONSTATE", Order = 15)]
    public MultiList<RegionState> RegionStates { get; private set; } = [];

    /// <summary>
    /// Community-related data, mostly composed of player reputation for each community in each region.
    /// </summary>
    [SaveFileElement("COMMUNITIES", Order = 14)]
    public CreatureCommunities Communities { get; set; } = new();

    /// <summary>
    /// Contains miscellaneous data mostly related to Iterator / Downpour events.
    /// </summary>
    [SaveFileElement("MISCWORLDSAVEDATA", Order = 21)]
    public MiscWorldSaveData MiscWorldSaveData { get; private set; } = new();

    /// <summary>
    /// Contains data that persists across player deaths.
    /// </summary>
    [SaveFileElement("DEATHPERSISTENTSAVEDATA", Order = 20)]
    public DeathPersistentSaveData DeathPersistentSaveData { get; private set; } = new();

    // TODO: Implement item deserialization
    /// <summary>
    /// Contains serialized strings of swallowed items
    /// </summary>
    [SaveFileElement("SWALLOWEDITEMS", Order = 16)]
    public RawValues SwallowedItems { get; set; } = new();

    /// <summary>
    /// Contains serialized strings of swallowed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDSWALLOWED", Order = 17)]
    public RawValues UnrecognizedSwallowedItems { get; set; } = new();

    // TODO: Implement grab deserialization
    /// <summary>
    /// Contains serialized strings of grabbed items and creatures <para/>
    /// </summary>
    [SaveFileElement("PLAYERGRASPS", Order = 18)]
    public RawValues PlayerGrasps { get; set; } = new();

    /// <summary>
    /// Contains serialized strings of grabbed items and creatures that were not recognized by the game. <para/>
    /// The game tries to parse them again on each save load.
    /// </summary>
    [SaveFileElement("UNRECOGNIZEDPLAYERGRASPS", Order = 19)]
    public RawValues UnrecognizedPlayerGrasps { get; set; } = new();

    /// <summary>
    /// The version of Rain World for this save.
    /// </summary>
    [SaveFileElement("VERSION", Order = 2)]
    public int GameVersion { get; set; } = 0;

    /// <summary>
    /// The initial version of Rain World for this save. <para/>
    /// PS: might be the same as VERSION.
    /// </summary>
    [SaveFileElement("INITVERSION", Order = 3)]
    public int InitialGameVersion { get; set; } = 0;

    /// <summary>
    /// The current world version for this save. <para/>
    /// Rain World will try updating old saves to the newest world version on load.
    /// </summary>
    [SaveFileElement("WORLDVERSION", Order = 4)]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// Random seed generated on save creation. <para/>
    /// This is mainly used to determine which pearl text will be used by Moon.
    /// </summary>
    [SaveFileElement("SEED", Order = 1)]
    public int Seed { get; set; } = 0;

    /// <summary>
    /// Contains data related to shelter dreams. <para/>
    /// This is missing entirely for slugcats that don't have dreams.
    /// </summary>
    [SaveFileElement("DREAMSSTATE", Order = 22)]
    public DreamsState? DreamsState { get; set; } = null;

    /// <summary>
    /// Stores the total number of food pips consumed during this playthough.
    /// </summary>
    [SaveFileElement("TOTFOOD", Order = 23)]
    public int TotalFoodEaten { get; set; } = 0;

    /// <summary>
    /// Stores the total time elapsed in seconds during this playthrough.
    /// </summary>
    [SaveFileElement("TOTTIME", Order = 24)]
    public int TotalTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// Stores the number of cycles played on the same world version. <para/>
    /// This is reset whenever the save world version is updated.
    /// </summary>
    [SaveFileElement("CURRVERCYCLES", Order = 25)]
    public int CyclesInCurrentWorldVersion { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of kills for each creature.
    /// </summary>
    [SaveFileElement("KILLS", ListDelimiter="<svC>", Order = 26)]
    public List<CreatureKillData> Kills { get; } = [];

    /// <summary>
    /// Indicates whenever the player received extra cycles from visiting Five Pebbles.
    /// </summary>
    [SaveFileElement("REDEXTRACYCLES", true, Order = 27)]
    public bool HunterExtraCycles { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player just beat the game. <para/>
    /// This is used to restore the player's food pips in a similar way to Fast Travels when continuing right after a win.
    /// </summary>
    [SaveFileElement("JUSTBEATGAME", true, Order = 28)]
    public bool GameRecentlyBeaten { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has a Citizen Drone following it. <para/>
    /// This is used for the Artificer campaign.
    /// </summary>
    [SaveFileElement("HASROBO", true, Order = 29)]
    public bool HasCitizenDrone { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player is wearing Moon's cloak.
    /// </summary>
    [SaveFileElement("CLOAK", true, Order = 30)]
    public bool IsWearingCloak { get; set; } = false;

    /// <summary>
    /// Tracks whenever Saint has reached max Karma cap and has seen the related karma dream.
    /// </summary>
    [SaveFileElement("KARMADREAM", true, Order = 31)]
    public bool KarmaDream { get; set; } = false;

    /// <summary>
    /// Tracks whenever pups will be forced to spawn for the next cycle.
    /// 0 = Gourmand not beaten / no effect
    /// 1 = Max allowed number of pups guaranteed to spawn next cycle
    /// 2 = Chance based pup spawns
    /// </summary>
    [SaveFileElement("FORCEPUPS", Order = 32)]
    public int ForcePupsNextCycle { get; set; } = 0;

    // TODO Implement detailed object tracker deserialization
    /// <summary>
    /// List used for tracking persistent objects, and resspawning them if they are lost. <para/>
    /// This works only if the Remix option is set.
    /// </summary>
    [SaveFileElement("OBJECTTRACKERS", ListDelimiter = "<svC>", Order = 33)]
    public List<string> ObjectTrackers { get; private set; } = [];

    /// <summary>
    /// Saved objects and critters in the world.
    /// </summary>
    [SaveFileElement("OBJECTS", ListDelimiter = "<svC>", SerializeIfEmpty = false, Order = 34)]
    public List<string> Objects { get; private set; } = [];

    /// <summary>
    /// Saved friendly creatures.
    /// </summary>
    [SaveFileElement("FRIENDS", ListDelimiter = "<svC>", Order = 35)]
    public List<string> Friends { get; private set; } = [];

    /// <summary>
    /// Tracks the list of slugcat encounters for Gourmand in the Outer Expanse.
    /// </summary>
    [SaveFileElement("OEENCOUNTERS", ListDelimiter = "<svC>", Order = 36)]
    public List<string> OuterExpanseEncounters { get; private set; } = [];

    /// <summary>
    /// The internal name of the slugcat campaign.
    /// </summary>
    [SaveFileElement("SAV STATE NUMBER", Order = 0)]
    public string SaveStateNumber { get; set; } = "???";

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
}
