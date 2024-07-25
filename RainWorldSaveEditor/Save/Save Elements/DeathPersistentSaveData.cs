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
    /// KARMA
    /// </summary>
    [SaveFileElement("KARMA")]
    public int Karma { get; set; } = 0;

    /// <summary>
    /// KARMACAP
    /// </summary>
    [SaveFileElement("KARMACAP")]
    public int KarmaCap { get; set; } = 0;

    /// <summary>
    /// REINFORCEDKARMA
    /// </summary>
    [SaveFileElement("REINFORCEDKARMA")]
    public bool HasReinforcedKarma { get; set; } = false;

    /// <summary>
    /// FLOWERPOS
    /// Position of Karma Flower created upon player death
    /// </summary>
    [SaveFileElement("FLOWERPOS")]
    public WorldCoordinate? KarmaFlowerPosition { get; set; }

    /// <summary>
    /// HASTHEMARK (valueless)
    /// </summary>
    [SaveFileElement("HASTHEMARK", true)]
    public bool HasMarkOfCommunication { get; set; } = false;

    /// <summary>
    /// FOODREPBONUS
    /// </summary>
    [SaveFileElement("FOODREPBONUS")]
    public int FoodReplenishBonus { get; set; } = 0;

    /// <summary>
    /// DDWORLDVERSION
    /// </summary>
    [SaveFileElement("DDWORLDVERSION")]
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// DEATHS
    /// </summary>
    [SaveFileElement("DEATHS")]
    public int Deaths { get; set; } = 0;

    /// <summary>
    /// SURVIVES
    /// </summary>
    [SaveFileElement("SURVIVES")]
    public int Survives { get; set; } = 0;

    /// <summary>
    /// QUITS
    /// </summary>
    [SaveFileElement("QUITS")]
    public int Quits { get; set; } = 0;

    /// <summary>
    /// REDSDEATH
    /// This value gets set automatically on death, but only makes sense for Hunter's campaign.
    /// If Hunter is not out of cycles, it gets cleared on load. Otherwise, it prevents loading the save file.
    /// </summary>
    [SaveFileElement("REDSDEATH")]
    public bool IsHunterDead { get; set; } = false;

    /// <summary>
    /// ASCENDED
    /// </summary>
    [SaveFileElement("ASCENDED", true)]
    public bool HasAscended { get; set; } = false;

    /// <summary>
    /// PHIRKC
    /// </summary>
    [SaveFileElement("PHIRKC")]
    public bool HasPebblesIncreasedHuntersKarma { get; set; } = false;

    /// <summary>
    /// FRIENDSAVEBONUS
    /// </summary>
    [SaveFileElement("FRIENDSAVEBONUS")]
    public int FriendsSaved { get; set; } = 0;

    /// <summary>
    /// DEATHTIME
    /// </summary>
    [SaveFileElement("DEATHTIME")]
    public int DeathTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// ALTENDING
    /// </summary>
    [SaveFileElement("ALTENDING")]
    public bool AltEndingAchieved { get; set; } = false;

    /// <summary>
    /// ZEROPEBBLES
    /// </summary>
    [SaveFileElement("ZEROPEBBLES", true)]
    public bool IsPebblesAscendedBySaint { get; set; } = false;

    /// <summary>
    /// LOOKSTOTHEDOOM
    /// </summary>
    [SaveFileElement("LOOKSTOTHEDOOM", true)]
    public bool IsMoonAscendedBySaint { get; set; } = false;

    [SaveFileElement("SLSiren")]
    public bool SLSiren_Unused { get; set; } = false;

    /// <summary>
    /// TIPS
    /// </summary>
    [SaveFileElement("TIPS")]
    public int TipCounter { get; set; } = 0;

    /// <summary>
    /// TIPSEED
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
