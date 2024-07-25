using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public bool HasReinforcedKarma { get; set; } = false;

    /// <summary>
    /// Position of Karma Flower created upon player death.
    /// </summary>
    [SaveFileElement("FLOWERPOS")]
    public WorldCoordinate? KarmaFlowerPosition { get; set; }

    /// <summary>
    /// Whenever the slugcat currently has the mark of communication.
    /// </summary>
    [SaveFileElement("HASTHEMARK", true)]
    public bool HasMarkOfCommunication { get; set; } = false;

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
    /// This value gets set automatically on death, but only makes sense for Hunter's campaign. <para/>
    /// If Hunter is not out of cycles, it gets cleared on load. Otherwise, it prevents loading the save file.
    /// </summary>
    [SaveFileElement("REDSDEATH")]
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
    [SaveFileElement("PHIRKC")]
    public bool HasPebblesIncreasedHuntersKarma { get; set; } = false;

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
    [SaveFileElement("SLSiren")]
    public bool SLSiren { get; set; } = false;

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

    /*
    private void ParseField(string key, string value)
    {
        // TODO Error handling for Parse functions
        switch (key)
        {
            case "KARMA":
                Karma = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "KARMACAP":
                KarmaCap = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "REINFORCEDKARMA":
                HasReinforcedKarma = value == "1";
                break;
            case "FLOWERPOS":
                {
                    // TODO handle less / more than 4 values
                    string[] values = value.Split('.');
                    KarmaFlowerPosition = new()
                    {
                        RoomName = values[0],
                        X = int.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Y = int.Parse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture),
                        AbstractNode = int.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture)
                    };
                }
                break;
            case "GHOSTS": //TODO
                break;
            case "SONGSPLAYRECORDS": //TODO
                break;
            case "SESSIONRECORDS": //TODO
                break;
            case "WINSTATE": //TODO
                break;
            case "CONSUMEDFLOWERS": //TODO
                break;
            case "HASTHEMARK":
                HasMarkOfCommunication = true;
                break;
            case "TUTMESSAGES": //TODO
                break;
            case "METERSSHOWN": //TODO
                break;
            case "FOODREPBONUS":
                FoodReplenishBonus = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "DDWORLDVERSION":
                WorldVersion = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "DEATHS":
                Deaths = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "SURVIVES":
                Survives = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "QUITS":
                Quits = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "DEATHPOSS": //TODO
                break;
            case "REDSDEATH":
                IsHunterDead = true;
                break;
            case "ASCENDED":
                HasAscended = true;
                break;
            case "PHIRKC":
                HasPebblesIncreasedHuntersKarma = true;
                break;
            case "UNLOCKEDGATES": //TODO
                break;
            case "FRIENDSAVEBONUS":
                FriendsSaved = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "DEATHTIME":
                DeathTimeInSeconds = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "ALTENDING":
                AltEndingAchieved = true;
                break;
            case "ZEROPEBBLES":
                IsPebblesAscendedBySaint = true;
                break;
            case "LOOKSTOTHEDOOM":
                IsMoonAscendedBySaint = true;
                break;
            case "SLSIREN":
                // This doesn't seem to be set or used by the game
                break;
            case "CHATLOGS": //TODO
                break;
            case "PREPEBCHATLOGS": //TODO
                break;
            case "TIPS":
                TipCounter = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            case "TIPSEED":
                TipSeed = int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
                break;
            default:
                UnrecognizedFields[key] = value;
                break;
        }
    }
    */
}
