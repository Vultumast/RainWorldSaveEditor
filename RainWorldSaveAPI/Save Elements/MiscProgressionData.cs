using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.SaveElements;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

public class MiscProgressionData : SaveElementContainer, IParsable<MiscProgressionData>
{
    [SaveFileElement("CURRENTSLUGCAT")]
    public string CurrentSelectedSave { get; set; } = "White";

    [SaveFileElement("SHELTERLIST", ListDelimiter = "<mpdC>")]
    public List<string> DiscoveredShelters { get; set; } = [];

    // TODO custom object for this
    [SaveFileElement("CONDITIONALSHELTERDATA", ListDelimiter = "<mpdC>")]
    public List<ConditionalShelterData> ConditionalShelterData { get; set; } = [];

    [SaveFileElement("MENUREGION", ListDelimiter = "<mpdC>")]
    public string? MenuRegion { get; set; } = null;

    // TODO backwards compatibility
    [SaveFileElement("LEVELTOKENS", ListDelimiter = ",")]
    public List<string> LevelTokens { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("SANDBOXTOKENS", ListDelimiter = ",")]
    public List<string> SandboxTokens { get; set; } = [];

    [SaveFileElement("CLASSTOKENS", ListDelimiter = ",")]
    public List<string> ClassTokens { get; set; } = [];

    [SaveFileElement("SAFARITOKENS", ListDelimiter = ",")]
    public List<string> SafariTokens { get; set; } = [];

    [SaveFileElement("LORE", ListDelimiter = ",")]
    public List<string> DecipheredPearls { get; set; } = [];

    [SaveFileElement("LOREP", ListDelimiter = ",")]
    public List<string> DecipheredPearlsArtificer { get; set; } = [];

    [SaveFileElement("LOREDM", ListDelimiter = ",")]
    public List<string> DecipheredPearlsSpearmaster { get; set; } = [];

    [SaveFileElement("LOREFUT", ListDelimiter = ",")]
    public List<string> DecipheredPearlsSaint { get; set; } = [];

    [SaveFileElement("BROADCASTS", ListDelimiter = ",")]
    public List<string> DiscoveredBroadcasts { get; set; } = [];

    // TODO INTEGERS
    // TODO INTEGERSMSC
    // TODO INTEGERSMMF

    /// <summary>
    /// Tracks if the player has done the Heartbreak / Fez secret ending for Saint.
    /// </summary>
    [SaveFileElement("HASDONEHEARTREBOOT", ListDelimiter = ",")]
    public List<string> HasDoneSaintFezEnding { get; set; } = [];

    [SaveFileElement("PLAYEDARENAS", ListDelimiter = "<mpdC>")]
    public List<string> PlayedArenaLevels { get; set; } = [];

    [SaveFileElement("CHARENAS", ListDelimiter = "<mpdC>")]
    public List<string> ChallengeArenaUnlocks { get; set; } = [];

    [SaveFileElement("CHCLEAR", ListDelimiter = "<mpdC>")]
    public List<string> CompletedChallenges { get; set; } = [];

    [SaveFileElement("CHCLEARTIMES", ListDelimiter = "<mpdC>")]
    public List<int> CompletedChallengeTimes { get; set; } = [];

    [SaveFileElement("CUSTCOLORS", IsRepeatableKey = true)]
    public List<ColorChoice> CustomColors { get; set; } = [];

    [SaveFileElement("CAMPAIGNTIME", IsRepeatableKey = true)]
    public List<CampaignTime> CampaignTime { get; set; } = [];

    /// <summary>
    /// Tracks the slugcat that gave Moon the cloak. <para/>
    /// In all other campaigns that are later in the timeline compared to this one, Moon will have the cloak automatically. <para/>
    /// By default, Moon has the cloak starting with Rivulet.
    /// </summary>
    [SaveFileElement("CLOAKTIMELINE")]
    public string? SlugcatThatGaveMoonTheCloak { get; set; } = null;

    /// <summary>
    /// Tracks the object that Saint carried in their stomach at the end of their campaign. <para/>
    /// The default value of "0" means they had no item in their stomach. <para/>
    /// Saint will start with this item in their stomach when restarting their campaign.
    /// </summary>
    [SaveFileElement("SAINTSTOMACH")]
    public string SaintObjectCarriedInStomach { get; set; } = "0";

    [SaveFileElement("VISITED", ListDelimiter = "<mpdC>")]
    public List<string> RegionsVisited { get; set; } = [];

    // TODO backwards compatibility
    /// <summary>
    /// Tracks the location where Hunter died for real. <para/>
    /// This location is used to spawn Karma Flowers in Survivor, Monk, Gourmand, and Inv's campaigns. <para/>
    /// This is also used to spawn Hunter Long Legs in Gourmand and Inv's campaigns.
    /// </summary>
    [SaveFileElement("REDSFLOWER")]
    public WorldCoordinate? HunterPermadeathLocation { get; set; } = null;

    public static MiscProgressionData Parse(string s, IFormatProvider? provider)
    {
        MiscProgressionData data = new();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<mpdB>", "<mpdA>"))
            ParseField(data, key, value);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MiscProgressionData result)
    {
        // Vultu: probably make this better
        result = Parse(s, provider);
        return true;
    }
}
