using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveEditor.Save;

public class DeathPersistentSaveData : SaveElementContainer, IParsable<DeathPersistentSaveData>
{
    public DeathPersistentSaveData() : base()
    {

    }

    /// <summary>
    /// The current karma level.
    /// </summary>
    [SaveFileElement("KARMA")]
    public int Karma { get; set; } = 0;

    /// <summary>
    /// The max karma level.
    /// </summary>
    [SaveFileElement("KARMACAP")]
    public int KarmaCap { get; set; } = 0;

    /// <summary>
    /// Whenever the karma is currently reinforced by a Karma Flower.
    /// </summary>
    [SaveFileElement("REINFORCEDKARMA")]
    public int HasReinforcedKarma
    {
        get => _hasReinforcedKarma ? 1 : 0;
        set => _hasReinforcedKarma = value != 0;
    }
    private bool _hasReinforcedKarma = false;

    /// <summary>
    /// Position of Karma Flower created upon player death.
    /// </summary>
    [SaveFileElement("FLOWERPOS")]
    public WorldCoordinate? KarmaFlowerPosition { get; set; }

    /// <summary>
    /// Current state of echoes in the world. <para/>
    /// 0 = Echo not met yet <para/>
    /// 1 = Echo area visited on a previous cycle <para/>
    /// 2 = Echo met and karma increased <para/>
    /// Some echoes can be met without visiting on a previous cycle. <para/>
    /// Hunter in particular can meet echoes without having to visit the area beforehand.
    /// </summary>
    [SaveFileElement("GHOSTS")]
    public Ghosts Ghosts { get; private set; } = new();

    /// <summary>
    /// Tracks when was a particular song last played.
    /// </summary>
    [SaveFileElement("SONGSPLAYRECORDS", ListDelimiter = "<dpC>")]
    public List<SongPlayRecord> SongPlayRecords { get; private set; } = [];

    /// <summary>
    /// Tracks information about game sessions, such as whenever the player survived, etc.
    /// </summary>
    [SaveFileElement("SESSIONRECORDS", ListDelimiter = "<dpC>")]
    public List<SessionRecord> SessionRecords { get; private set; } = [];

    /// <summary>
    /// Tracks the progress for each passage.
    /// </summary>
    [SaveFileElement("WINSTATE")]
    public WinState WinState { get; private set; } = new();

    /// <summary>
    /// Tracks Karma Flowers that have been consumed by the player in the world. <para/>
    /// This is used to track when they should respawn.
    /// </summary>
    [SaveFileElement("CONSUMEDFLOWERS", ListDelimiter = "<dpC>")]
    public List<ConsumedItem> ConsumedKarmaFlowers { get; private set; } = [];

    /// <summary>
    /// Whenever the slugcat currently has the mark of communication.
    /// </summary>
    [SaveFileElement("HASTHEMARK", true)]
    public bool HasMarkOfCommunication { get; set; } = false;

    /// <summary>
    /// Contains a list of tutorial messages shown to the player.
    /// </summary>
    [SaveFileElement("TUTMESSAGES")]
    public TutorialMessages TutorialMessages { get; set; } = new();

    /// <summary>
    /// Contains a list of passage meters that appeared on the sleep screen. <para/>
    /// Mainly used to force the player to watch the entire karma screen sequence.
    /// </summary>
    [SaveFileElement("METERSSHOWN")]
    public PassageMetersShown PassageMetersShown { get; set; } = new();

    /// <summary>
    /// Gets incremented on each death at minimum karma, gets reset on a survived cycle. <para/>
    /// Survivor has a 50% chance to increase this on death that is not at minimum karma, and Monk has a 100% chance. <para/>
    /// This causes food-related consumables and bats to replenish at a faster rate.
    /// </summary>
    [SaveFileElement("FOODREPBONUS")]
    public int FoodReplenishBonus { get; set; } = 0;

    /// <summary>
    /// Death persistent data specific world version. <para/>
    /// Similar to save state world version, Rain World will try updating old saves to the newest world version on load.
    /// </summary>
    [SaveFileElement("DDWORLDVERSION")]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of player deaths in this playthrough.
    /// </summary>
    [SaveFileElement("DEATHS")]
    public int Deaths { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of cycles survived in this playthrough.
    /// </summary>
    [SaveFileElement("SURVIVES")]
    public int Survives { get; set; } = 0;

    /// <summary>
    /// Tracks the total number of cycles abandoned in this playthrough.
    /// </summary>
    [SaveFileElement("QUITS")]
    public int Quits { get; set; } = 0;

    /// <summary>
    /// List of world positions where the player has died. <para/>
    /// This is used to render the marks on the map. <para/>
    /// Death positions use the AbstractNode property to track their cycle age, and are removed after 7 cycles.
    /// </summary>
    [SaveFileElement("DEATHPOSS", ListDelimiter = "<dpC>")]
    public List<WorldCoordinate> DeathPositions { get; private set; } = [];

    /// <summary>
    /// This value gets set automatically on death, but only makes sense for Hunter's campaign. <para/>
    /// If Hunter is not out of cycles, it gets cleared on load. Otherwise, it prevents loading the save file.
    /// </summary>
    [SaveFileElement("REDSDEATH", true)]
    public bool IsHunterDead { get; set; } = false;

    /// <summary>
    /// Whenever the player has ascended in this playthrough.
    /// </summary>
    [SaveFileElement("ASCENDED", true)]
    public bool HasAscended { get; set; } = false;

    /// <summary>
    /// Whenever Pebbles has increased Hunter's karma cap by one step. <para/>
    /// This is used to prevent Pebbles from increasing karma cap again if the player fails the previous cycle.
    /// </summary>
    [SaveFileElement("PHIRKC", true)]
    public bool HasPebblesIncreasedHuntersKarma { get; set; } = false;

    /// <summary>
    /// List of gates that have been unlocked in this playthrough. <para/>
    /// Gates can be unlocked by default by Monk, and by any other slugcat if the Remix option is enabled.
    /// </summary>
    [SaveFileElement("UNLOCKEDGATES", ListDelimiter = "<dpC>")]
    public List<string> UnlockedGates { get; private set; } = [];

    /// <summary>
    /// Number of friends sheltered during this playthrough <para/>
    /// Used for end game score calculation.
    /// </summary>
    [SaveFileElement("FRIENDSAVEBONUS")]
    public int FriendsSaved { get; set; } = 0;

    /// <summary>
    /// Tracks the total amount of time spent in a dead state.
    /// </summary>
    [SaveFileElement("DEATHTIME")]
    public int DeathTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// Tracks whenever the alternate ending for slugcats has been achieved. <para/>
    /// Relevant for Survivor, Monk, Rivulet, Artificer, Spearmaster and Gourmand.
    /// </summary>
    [SaveFileElement("ALTENDING")]
    public bool AltEndingAchieved { get; set; } = false;

    /// <summary>
    /// Tracks whenever Five Pebbles has lost his marbles at the hands of Saint.
    /// </summary>
    [SaveFileElement("ZEROPEBBLES", true)]
    public bool IsPebblesAscendedBySaint { get; set; } = false;

    /// <summary>
    /// Tracks whenever Looks to the Moon has been launched to the moon by Saint.
    /// </summary>
    [SaveFileElement("LOOKSTOTHEDOOM", true)]
    public bool IsMoonAscendedBySaint { get; set; } = false;

    /// <summary>
    /// Unused. Also unknown what the intended usage was going to be, if any.
    /// </summary>
    [SaveFileElement("SLSIREN", true)]
    public bool SLSiren { get; set; } = false;

    /// <summary>
    /// A list of broadcasts that have been read by the player.
    /// </summary>
    [SaveFileElement("CHATLOGS", ListDelimiter = ",")]
    public List<string> ChatLogsRead { get; private set; } = [];

    /// <summary>
    /// A list of broadcasts that have been read by the player, before ever interacting with Five Pebbles.
    /// </summary>
    [SaveFileElement("PREPEBCHATLOGS", ListDelimiter = ",")]
    public List<string> ChatLogsReadBeforeFP { get; private set; } = [];

    /// <summary>
    /// Counter used for displaying tips in-game.
    /// </summary>
    [SaveFileElement("TIPS")]
    public int TipCounter { get; set; } = 0;

    /// <summary>
    /// Random seed set on game start used for displaying tips in-game.
    /// </summary>
    [SaveFileElement("TIPSEED")]
    public int TipSeed { get; set; } = 0;

    public string Write()
    {
        throw new NotImplementedException();
    }

    public static DeathPersistentSaveData Parse(string s, IFormatProvider? provider)
    {
        DeathPersistentSaveData data = new DeathPersistentSaveData();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<dpB>", "<dpA>"))
            ParseField(data, key, value);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DeathPersistentSaveData result)
    {
        // Vultu: probably make this better
        result = Parse(s, provider);
        return true;
    }
}
