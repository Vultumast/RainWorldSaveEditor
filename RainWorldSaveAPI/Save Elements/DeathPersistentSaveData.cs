using System.Diagnostics.CodeAnalysis;
using System.Text;
using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.Save_Elements;

namespace RainWorldSaveAPI.SaveElements;
public class DeathPersistentSaveData : SaveElementContainer, IRWSerializable<DeathPersistentSaveData>
{
    /// <summary>
    /// This value gets set automatically on death, but only makes sense for Hunter's campaign. <para/>
    /// If Hunter is not out of cycles, it gets cleared on load. Otherwise, it prevents loading the save file.
    /// </summary>
    [SaveField(0, "REDSDEATH")]
    public bool IsHunterDead { get; set; } = false;

    /// <summary>
    /// Whenever the player has ascended in this playthrough.
    /// </summary>
    [SaveField(1, "ASCENDED")]
    public bool HasAscended { get; set; } = false;

    /// <summary>
    /// Whenever the karma is currently reinforced by a Karma Flower.
    /// <para>This IS a bool, but is serialized as a 0 for false and a 1 for true</para>
    /// </summary>
    [SaveField(2, "REINFORCEDKARMA")]
    public int HasReinforcedKarma
    {
        get => _hasReinforcedKarma ? 1 : 0;
        set => _hasReinforcedKarma = value != 0;
    }
    private bool _hasReinforcedKarma = false;

    /// <summary>
    /// The current karma level.
    /// </summary>
    [SaveField(3, "KARMA")]
    public int Karma { get; set; } = 0;

    /// <summary>
    /// The max karma level.
    /// </summary>
    [SaveField(4, "KARMACAP")]
    public int KarmaCap { get; set; } = 0;

    /// <summary>
    /// Position of Karma Flower created upon player death.
    /// </summary>
    [SaveField(5, "FLOWERPOS")]
    public WorldCoordinate? KarmaFlowerPosition { get; set; }

    /// <summary>
    /// Whenever the slugcat currently has the mark of communication.
    /// </summary>
    [SaveField(6, "HASTHEMARK")]
    public bool HasMarkOfCommunication { get; set; } = false;

    /// <summary>
    /// Current state of echoes in the world. <para/>
    /// 0 = Echo not met yet <para/>
    /// 1 = Echo area visited on a previous cycle <para/>
    /// 2 = Echo met and karma increased <para/>
    /// Some echoes can be met without visiting on a previous cycle. <para/>
    /// Hunter in particular can meet echoes without having to visit the area beforehand.
    /// </summary>
    [SaveField(7, "GHOSTS")] // This can be empty if there are no ghosts, apparently?
    public Echos Echos { get; private set; } = new();

    /// <summary>
    /// Tracks when was a particular song last played.
    /// </summary>
    [SaveField(8, "SONGSPLAYRECORDS", ListDelimiter = "<dpC>")]
    public List<SongPlayRecord> SongPlayRecords { get; private set; } = [];

    /// <summary>
    /// Tracks information about game sessions, such as whenever the player survived, etc.
    /// </summary>
    [SaveField(9, "SESSIONRECORDS", ListDelimiter = "<dpC>")]
    public List<SessionRecord> SessionRecords { get; private set; } = [];

    /// <summary>
    /// Tracks the progress for each passage.
    /// </summary>
    [SaveField(10, "WINSTATE")]
    public WinState WinState { get; private set; } = new();

    /// <summary>
    /// Tracks Karma Flowers that have been consumed by the player in the world. <para/>
    /// This is used to track when they should respawn.
    /// </summary>
    [SaveField(11, "CONSUMEDFLOWERS", ListDelimiter = "<dpC>")]
    public List<ConsumedItem> ConsumedKarmaFlowers { get; private set; } = [];

    /// <summary>
    /// Contains a list of tutorial messages shown to the player.
    /// </summary>
    [SaveField(12, "TUTMESSAGES")]
    public TutorialMessages TutorialMessages { get; set; } = new();

    /// <summary>
    /// Contains a list of passage meters that appeared on the sleep screen. <para/>
    /// Mainly used to force the player to watch the entire karma screen sequence.
    /// </summary>
    [SaveField(13, "METERSSHOWN")]
    public PassageMetersShown PassageMetersShown { get; set; } = new();

    /// <summary>
    /// Gets incremented on each death at minimum karma, gets reset on a survived cycle. <para/>
    /// Survivor has a 50% chance to increase this on death that is not at minimum karma, and Monk has a 100% chance. <para/>
    /// This causes food-related consumables and bats to replenish at a faster rate.
    /// </summary>
    [SaveField(14, "FOODREPBONUS")]
    public IntSerializeIfNotZero FoodReplenishBonus { get; set; } = new();

    /// <summary>
    /// Death persistent data specific world version. <para/>
    /// Similar to save state world version, Rain World will try updating old saves to the newest world version on load.
    /// </summary>
    [SaveField(15, "DDWORLDVERSION")]
    public IntSerializeIfNotZero WorldVersion { get; set; } = new();

    /// <summary>
    /// Tracks the total number of player deaths in this playthrough.
    /// </summary>
    [SaveField(16, "DEATHS")]
    public int Deaths { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of cycles survived in this playthrough.
    /// </summary>
    [SaveField(17, "SURVIVES")]
    public int Survives { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of cycles abandoned in this playthrough.
    /// </summary>
    [SaveField(18, "QUITS")]
    public int Quits { get; set; } = 0;

    /// <summary>
    /// Whenever Pebbles has increased Hunter's karma cap by one step. <para/>
    /// This is used to prevent Pebbles from increasing karma cap again if the player fails the previous cycle.
    /// </summary>
    [SaveField(19, "PHIRKC")]
    public bool HasPebblesIncreasedHuntersKarma { get; set; } = false;

    /// <summary>
    /// List of gates that have been unlocked in this playthrough. <para/>
    /// Gates can be unlocked by default by Monk, and by any other slugcat if the Remix option is enabled.
    /// </summary>
    [SaveField(20, "UNLOCKEDGATES", ListDelimiter = "<dpC>")]
    public List<string> UnlockedGates { get; private set; } = [];

    /// <summary>
    /// List of world positions where the player has died. <para/>
    /// This is used to render the marks on the map. <para/>
    /// Death positions use the AbstractNode property to track their cycle age, and are removed after 7 cycles.
    /// </summary>
    [SaveField(21, "DEATHPOSS", ListDelimiter = "<dpC>")]
    public List<WorldCoordinate> DeathPositions { get; private set; } = [];

    /// <summary>
    /// Tracks whenever the alternate ending for slugcats has been achieved. <para/>
    /// Relevant for Survivor, Monk, Rivulet, Artificer, Spearmaster and Gourmand.
    /// </summary>
    [SaveField(22, "ALTENDING")]
    public bool AltEndingAchieved { get; set; } = false;

    /// <summary>
    /// Tracks whenever Five Pebbles has lost his marbles at the hands of Saint.
    /// </summary>
    [SaveField(23, "ZEROPEBBLES")]
    public bool IsPebblesAscendedBySaint { get; set; } = false;

    /// <summary>
    /// Tracks whenever Looks to the Moon has been launched to the moon by Saint.
    /// </summary>
    [SaveField(24, "LOOKSTOTHEDOOM")]
    public bool IsMoonAscendedBySaint { get; set; } = false;

    /// <summary>
    /// Unused. Also unknown what the intended usage was going to be, if any.
    /// </summary>
    [SaveField(25, "SLSIREN")]
    public bool SLSiren { get; set; } = false;

    /// <summary>
    /// Tracks the total amount of time spent in a dead state.
    /// </summary>
    [SaveField(26, "DEATHTIME")]
    public int DeathTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// Number of friends sheltered during this playthrough <para/>
    /// Used for end game score calculation.
    /// </summary>
    [SaveField(27, "FRIENDSAVEBONUS")]
    public IntSerializeIfNotZero FriendsSaved { get; set; } = new();

    /// <summary>
    /// A list of broadcasts that have been read by the player.
    /// </summary>
    [SaveField(28, "CHATLOGS", ListDelimiter = ",", SerializeIfEmpty = true)]
    public List<string> ChatLogsRead { get; private set; } = [];

    /// <summary>
    /// A list of broadcasts that have been read by the player, before ever interacting with Five Pebbles.
    /// </summary>
    [SaveField(29, "PREPEBCHATLOGS", ListDelimiter = ",", SerializeIfEmpty = true)]
    public List<string> ChatLogsReadBeforeFP { get; private set; } = [];

    /// <summary>
    /// Counter used for displaying tips in-game.
    /// </summary>
    [SaveField(30, "TIPS")]
    public int TipCounter { get; set; } = 0;

    /// <summary>
    /// Random seed set on game start used for displaying tips in-game.
    /// </summary>
    [SaveField(31, "TIPSEED")]
    public int TipSeed { get; set; } = 0;

    public string Write()
    {
        throw new NotImplementedException();
    }

    public static DeathPersistentSaveData Deserialize(string key, string[] values, SerializationContext? context)
    {
        DeathPersistentSaveData data = new();

        data.DeserializeFields(values[0], "<dpB>", "<dpA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<dpB>", "<dpA>")
        ];

        return true;
    }

    protected override void DeserializeUnrecognizedField(string key, string[] values)
    {
        if (key.Trim() != "")
            UnrecognizedFields.Add((key, values));
    }
}
