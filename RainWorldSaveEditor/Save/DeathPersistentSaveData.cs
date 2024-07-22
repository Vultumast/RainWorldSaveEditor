using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveEditor.Save;

public class DeathPersistentSaveData
{
    public Dictionary<string, string> UnrecognizedFields { get; } = [];

    /// <summary>
    /// KARMA
    /// </summary>
    public int Karma { get; set; } = 0;

    /// <summary>
    /// KARMACAP
    /// </summary>
    public int KarmaCap { get; set; } = 0;

    /// <summary>
    /// REINFORCEDKARMA
    /// </summary>
    public bool HasReinforcedKarma { get; set; } = false;

    /// <summary>
    /// HASTHEMARK (valueless)
    /// </summary>
    public bool HasMarkOfCommunication { get; set; } = false;

    /// <summary>
    /// FOODREPBONUS
    /// </summary>
    public int FoodReplenishBonus { get; set; } = 0;

    /// <summary>
    /// DDWORLDVERSION
    /// </summary>
    public int WorldVersion { get; set; } = 0;

    /// <summary>
    /// DEATHS
    /// </summary>
    public int Deaths { get; set; } = 0;

    /// <summary>
    /// SURVIVES
    /// </summary>
    public int Survives { get; set; } = 0;

    /// <summary>
    /// QUITS
    /// </summary>
    public int Quits { get; set; } = 0;

    /// <summary>
    /// REDSDEATH
    /// This value gets set automatically on death, but only makes sense for Hunter's campaign.
    /// If Hunter is not out of cycles, it gets cleared on load. Otherwise, it prevents loading the save file.
    /// </summary>
    public bool IsHunterDead { get; set; } = false;

    /// <summary>
    /// ASCENDED
    /// </summary>
    public bool HasAscended { get; set; } = false;

    /// <summary>
    /// PHIRKC
    /// </summary>
    public bool HasPebblesIncreasedHuntersKarma { get; set; } = false;

    /// <summary>
    /// FRIENDSAVEBONUS
    /// </summary>
    public int FriendsSaved { get; set; } = 0;

    /// <summary>
    /// DEATHTIME
    /// </summary>
    public int DeathTimeInSeconds { get; set; } = 0;

    /// <summary>
    /// ALTENDING
    /// </summary>
    public bool AltEndingAchieved { get; set; } = false;

    /// <summary>
    /// ZEROPEBBLES
    /// </summary>
    public bool IsPebblesAscendedBySaint { get; set; } = false;

    /// <summary>
    /// LOOKSTOTHEDOOM
    /// </summary>
    public bool IsMoonAscendedBySaint { get; set; } = false;

    /// <summary>
    /// TIPS
    /// </summary>
    public int TipCounter { get; set; } = 0;

    /// <summary>
    /// TIPSEED
    /// </summary>
    public int TipSeed { get; set; } = 0;

    public void Read(string data)
    {
        foreach ((var key, var value) in SaveUtils.GetFields(data, "<dpB>", "<dpA>"))
        {
            ParseField(key, value);
        }
    }

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
            case "FLOWERPOS": //TODO
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

    public string Write()
    {
        throw new NotImplementedException();
    }
}
