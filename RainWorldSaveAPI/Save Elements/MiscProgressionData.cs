﻿using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

public class MiscProgressionData : SaveElementContainer, IRWSerializable<MiscProgressionData>
{
    [SaveFileElement("CURRENTSLUGCAT", Order = 0)]
    public string CurrentSelectedSave { get; set; } = "White";

    // TODO custom object for this
    [SaveFileElement("SHELTERLIST", Order = 1)]
    public MultiList<RawValues> DiscoveredShelters { get; set; } = [];

    // TODO custom object for this
    [SaveFileElement("CONDITIONALSHELTERDATA", ListDelimiter = "<mpdC>", Order = 2)]
    public List<ConditionalShelterData> ConditionalShelterData { get; set; } = [];

    [SaveFileElement("MENUREGION", ListDelimiter = "<mpdC>", Order = 26)]
    public string? MenuRegion { get; set; } = null;

    // TODO backwards compatibility
    [SaveFileElement("LEVELTOKENS", ListDelimiter = ",", Order = 3)]
    public List<string> LevelTokens { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("SANDBOXTOKENS", ListDelimiter = ",", Order = 4)]
    public List<string> SandboxTokens { get; set; } = [];

    [SaveFileElement("CLASSTOKENS", ListDelimiter = ",", Order = 5)]
    public List<string> ClassTokens { get; set; } = [];

    [SaveFileElement("SAFARITOKENS", ListDelimiter = ",", Order = 6)]
    public List<string> SafariTokens { get; set; } = [];

    [SaveFileElement("LORE", ListDelimiter = ",", Order = 10)]
    public List<string> DecipheredPearls { get; set; } = [];

    [SaveFileElement("LOREP", ListDelimiter = ",", Order = 11)]
    public List<string> DecipheredPearlsArtificer { get; set; } = [];

    [SaveFileElement("LOREDM", ListDelimiter = ",", Order = 12)]
    public List<string> DecipheredPearlsSpearmaster { get; set; } = [];

    [SaveFileElement("LOREFUT", ListDelimiter = ",", Order = 13)]
    public List<string> DecipheredPearlsSaint { get; set; } = [];

    [SaveFileElement("BROADCASTS", ListDelimiter = ",", Order = 15)]
    public List<string> DiscoveredBroadcasts { get; set; } = [];

    [SaveFileElement("INTEGERS", Order = 22)]
    public GenericIntegerArray Integers { get; set; } = new();

    public int WatchedSleepScreens
    {
        get => Integers.TryGet(1);
        set => Integers.TrySet(1, value);
    }

    public int WatchedDeathScreens
    {
        get => Integers.TryGet(2);
        set => Integers.TrySet(2, value);
    }

    public int WatchedDeathScreensWithFlower
    {
        get => Integers.TryGet(3);
        set => Integers.TrySet(3, value);
    }

    public int WatchedStarvationScreens
    {
        get => Integers.TryGet(4);
        set => Integers.TrySet(4, value);
    }

    public int StarvationTutorialCounter
    {
        get => Integers.TryGet(5);
        set => Integers.TrySet(5, value);
    }

    public int WarnedAboutKarmaLossOnExit
    {
        get => Integers.TryGet(6);
        set => Integers.TrySet(6, value);
    }

    public bool HunterHasVisitedPebbles
    {
        get => Integers.TryGet(7) == 1;
        set => Integers.TrySet(7, value ? 1 : 0);
    }

    public bool HunterUnlocked
    {
        get => Integers.TryGet(8) == 1;
        set => Integers.TrySet(8, value ? 1 : 0);
    }

    public bool LookedForOldVersionSaveFile
    {
        get => Integers.TryGet(9) == 1;
        set => Integers.TrySet(9, value ? 1 : 0);
    }

    public int HunterCarnivoreTutorial
    {
        get => Integers.TryGet(10);
        set => Integers.TrySet(10, value);
    }

    [SaveFileElement("INTEGERSMSC", Order = 24)]
    public GenericIntegerArray IntegersMSC { get; set; } = new();

    public int PrePebblesBroadcasts
    {
        get => IntegersMSC.TryGet(0);
        set => IntegersMSC.TrySet(0, value);
    }

    public int PostPebblesBroadcasts
    {
        get => IntegersMSC.TryGet(1);
        set => IntegersMSC.TrySet(1, value);
    }

    public bool HasBeatenSpearmaster
    {
        get => IntegersMSC.TryGet(3) > 0;
        set => IntegersMSC.TrySet(3, value ? Math.Max(1, IntegersMSC.TryGet(3)) : 0);
    }

    public bool HasBeatenSpearmasterWithAltEnding
    {
        get => IntegersMSC.TryGet(3) > 1;
        set => IntegersMSC.TrySet(3, value ? 2 : Math.Clamp(IntegersMSC.TryGet(3), 0, 1));
    }

    public bool HasBeatenArtificer
    {
        get => IntegersMSC.TryGet(4) == 1;
        set => IntegersMSC.TrySet(4, value ? 1 : 0);
    }

    public bool HasBeatenGourmand
    {
        get => IntegersMSC.TryGet(5) > 0;
        set => IntegersMSC.TrySet(5, value ? Math.Max(1, IntegersMSC.TryGet(3)) : 0);
    }

    public bool HasBeatenGourmandWithFoodQuest
    {
        get => IntegersMSC.TryGet(5) > 1;
        set => IntegersMSC.TrySet(5, value ? 2 : Math.Clamp(IntegersMSC.TryGet(3), 0, 1));
    }

    public bool HasBeatenRivulet
    {
        get => IntegersMSC.TryGet(6) == 1;
        set => IntegersMSC.TrySet(6, value ? 1 : 0);
    }

    public bool HasBeatenSaint
    {
        get => IntegersMSC.TryGet(7) == 1;
        set => IntegersMSC.TrySet(7, value ? 1 : 0);
    }

    public bool HasBeatenSurvivor
    {
        get => IntegersMSC.TryGet(8) == 1;
        set => IntegersMSC.TrySet(8, value ? 1 : 0);
    }

    public bool HasBeatenHunter
    {
        get => IntegersMSC.TryGet(9) == 1;
        set => IntegersMSC.TrySet(9, value ? 1 : 0);
    }

    public int SurvivorEndingID
    {
        get => IntegersMSC.TryGet(10);
        set => IntegersMSC.TrySet(10, value);
    }

    public int MonkEndingID
    {
        get => IntegersMSC.TryGet(11);
        set => IntegersMSC.TrySet(11, value);
    }

    public int SurvivorPupsAtEnding
    {
        get => IntegersMSC.TryGet(12);
        set => IntegersMSC.TrySet(12, value);
    }

    public int ArtificerEndingID
    {
        get => IntegersMSC.TryGet(13);
        set => IntegersMSC.TrySet(13, value);
    }

    [SaveFileElement("INTEGERSMMF", Order = 23)]
    public GenericIntegerArray IntegersMMF { get; set; } = new();

    public bool GateTutorialShown
    {
        get => IntegersMMF.TryGet(0) == 1;
        set => IntegersMMF.TrySet(0, value ? 1 : 0);
    }

    public int ReturnExplorationTutorialCounter
    {
        get => IntegersMMF.TryGet(1, 3); // Default value of 3 for some reason
        set => IntegersMMF.TrySet(1, value);
    }

    public bool SporePuffTutorialShown
    {
        get => IntegersMMF.TryGet(2) == 1;
        set => IntegersMMF.TrySet(2, value ? 1 : 0);
    }

    public bool DeerControlTutorialShown
    {
        get => IntegersMMF.TryGet(3) == 1;
        set => IntegersMMF.TrySet(3, value ? 1 : 0);
    }

    /// <summary>
    /// Tracks if the player has done the Heartbreak / Fez secret ending for Saint.
    /// </summary>
    [SaveFileElement("HASDONEHEARTREBOOT", Order = 14)]
    public int HasDoneSaintFezEnding { get; set; } = 0;

    [SaveFileElement("PLAYEDARENAS", ListDelimiter = "<mpdC>", Order = 8)]
    public List<string> PlayedArenaLevels { get; set; } = [];

    [SaveFileElement("CHARENAS", ListDelimiter = "<mpdC>", Order = 16)]
    public List<string> ChallengeArenaUnlocks { get; set; } = [];

    [SaveFileElement("CHCLEAR", Order = 17)]
    public GenericBoolArray CompletedChallenges { get; set; } = new();

    [SaveFileElement("CHCLEARTIMES", ListDelimiter = "<mpdC>", Order = 18)]
    public List<int> CompletedChallengeTimes { get; set; } = [];

    [SaveFileElement("CUSTCOLORS", Order = 19)]
    public MultiList<ColorChoice> CustomColors { get; set; } = [];

    [SaveFileElement("CAMPAIGNTIME", Order = 20)]
    public MultiList<CampaignTime> CampaignTime { get; set; } = [];

    /// <summary>
    /// Tracks the slugcat that gave Moon the cloak. <para/>
    /// In all other campaigns that are later in the timeline compared to this one, Moon will have the cloak automatically. <para/>
    /// By default, Moon has the cloak starting with Rivulet.
    /// </summary>
    [SaveFileElement("CLOAKTIMELINE", Order = 9)]
    public string? SlugcatThatGaveMoonTheCloak { get; set; } = null;

    /// <summary>
    /// Tracks the object that Saint carried in their stomach at the end of their campaign. <para/>
    /// The default value of "0" means they had no item in their stomach. <para/>
    /// Saint will start with this item in their stomach when restarting their campaign.
    /// </summary>
    [SaveFileElement("SAINTSTOMACH", Order = 7)]
    public string SaintObjectCarriedInStomach { get; set; } = "0";

    [SaveFileElement("VISITED", Order = 21)]
    public RawValues RegionsVisited { get; set; } = new();

    // TODO backwards compatibility
    /// <summary>
    /// Tracks the location where Hunter died for real. <para/>
    /// This location is used to spawn Karma Flowers in Survivor, Monk, Gourmand, and Inv's campaigns. <para/>
    /// This is also used to spawn Hunter Long Legs in Gourmand and Inv's campaigns.
    /// </summary>
    [SaveFileElement("REDSFLOWER", Order = 25)]
    public WorldCoordinate? HunterPermadeathLocation { get; set; } = null;

    public static MiscProgressionData Deserialize(string key, string[] values, SerializationContext? context)
    {
        MiscProgressionData data = new();

        data.DeserializeFields(values[0], "<mpdB>", "<mpdA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<mpdB>", "<mpdA>")
        ];

        return true;
    }
}
