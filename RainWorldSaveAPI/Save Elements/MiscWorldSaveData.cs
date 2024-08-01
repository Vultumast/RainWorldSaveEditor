using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

public class MiscWorldSaveData : SaveElementContainer, IParsable<MiscWorldSaveData>
{
    /// <summary>
    /// Tracks the number of times the player has talked with Five Pebbles.
    /// </summary>
    [SaveFileElement("SSaiConversationsHad", Order = 0)]
    public int TimesTalkedWithFivePebbles { get; set; } = 0;

    /// <summary>
    /// Tracks the number of times the player has been thrown out by Five Pebbles. <para/>
    /// For most slugcats, FP becomes hostile after throwing them out once and will kill them on subsequent visits.
    /// </summary>
    [SaveFileElement("SSaiThrowOuts", Order = 1)]
    public int TimesKickedOutByFivePebbles { get; set; } = 0;

    /// <summary>
    /// Looks to the Moon's current state.
    /// </summary>
    [SaveFileElement("SLaiState", Order = 2)]
    public LooksToTheMoonState? LooksToTheMoonState { get; set; } = null;

    /// <summary>
    /// The guiding overseer's current state.
    /// </summary>
    [SaveFileElement("playerGuideState", Order = 3)]
    public PlayerGuideState PlayerGuideState { get; set; } = new();

    /// <summary>
    /// Tracks whenever Hunter successfully delivered the Green Neuron to Looks to the Moon and revived her.
    /// </summary>
    [SaveFileElement("MOONREVIVED", true, Order = 4)]
    public bool HasRevivedLooksToTheMoon { get; set; } = false;

    /// <summary>
    /// Tracks whenever Hunter visited Five Pebbles while having the Green Neuron.
    /// </summary>
    [SaveFileElement("PEBBLESHELPED", true, Order = 5)]
    public bool HasFivePebblesSeenGreenNeuron { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player visited Five Pebbles via the memory arrays entrance instead of the utility shaft. <para/>
    /// This flag is set specifically if the player enters room SS_D02.
    /// </summary>
    [SaveFileElement("MEMORYFROLICK", true, Order = 6)]
    public bool HasFrolickedInMemoryArrays { get; set; } = false;

    /// <summary>
    /// Tracks the number of cycles passed since first conversation with Five Pebbles. <para/>
    /// Seems to be used for tracking Spearmaster's pearl wound recovery.
    /// </summary>
    [SaveFileElement("CyclesSinceSSai", Order = 7)]
    public int CyclesSinceFirstFivePebblesVisit { get; set; } = 0;

    /// <summary>
    /// Tracks whenever Rivulet removed the Rarefaction Cell from Five Pebbles's generator.
    /// </summary>
    [SaveFileElement("ENERGYRAILOFF", true, Order = 8)]
    public bool HasRemovedRarefactionCell { get; set; } = false;

    /// <summary>
    /// Tracks Five Pebbles's conversation state related to the Rarefaction Cell. <para/>
    /// 0 = Default state <para/>
    /// 1 = Pebbles told Rivulet about the cell on their first visit <para/>
    /// 2 = Pebbles acknowledged that the cell has been removed on Rivulet's first visit (without them holding the cell) <para/>
    /// 3 = Pebbles saw Rivulet hold the cell on one of the visits, or Pebbles acknowledged that the cell has been removed after being visited again
    /// </summary>
    [SaveFileElement("EnergySeenState", Order = 9)]
    public int RarefactionCellConversationState { get; set; } = 0;

    /// <summary>
    /// Tracks whenever Rivulet delivered the Rarefaction Cell to Moon's core.
    /// </summary>
    [SaveFileElement("MOONHEART", true, Order = 10)]
    public bool DeliveredRarefactionCellToMoon { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has given Moon the Cloak.
    /// </summary>
    [SaveFileElement("MOONROBE", true, Order = 11)]
    public bool HasGivenMoonTheCloak { get; set; } = false;

    /// <summary>
    /// Tracks whenever Moon has encoded Spearmaster's pearl with a new message.
    /// </summary>
    [SaveFileElement("SMPEARLTAGGED", true, Order = 12)]
    public bool HasMoonEncodedSpearmasterPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever Five Pebbles has talked about his music pearl.
    /// </summary>
    [SaveFileElement("HALCYONTALK", true, Order = 13)]
    public bool HasDiscussedFivePebblesMusicPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has stolen Five Pebbles's music pearl. <para/>
    /// Truly despicable behaviour indeed.
    /// </summary>
    [SaveFileElement("HALCYONSTOLE", true, Order = 14)]
    public bool HasStolenFivePebblesMusicPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has talked with Five Pebbles after delivering the Rarefaction Cell to Moon.
    /// </summary>
    [SaveFileElement("PEBRIVPOST", true, Order = 15)]
    public bool HasTalkedWithFivePebblesInRivuletPostgame { get; set; } = false;

    /// <summary>
    /// Tracks whenever Saint has talked with Five Pebbles and / or Looks to the Moon in Rubicon's final room.
    /// </summary>
    [SaveFileElement("HRMELT", true, Order = 16)]
    public bool TalkedWithOraclesInRubicon { get; set; } = false;

    /// <summary>
    /// Tracks how many cycles have passed since a Slugpup has spawned. <para/>
    /// This is used to guarantee Slugpup spawns after 25 cycles (5 for Hunter).
    /// </summary>
    [SaveFileElement("CyclesSinceSlugpup", Order = 17)]
    public int CyclesSinceLastSlugpupSpawn { get; set; } = 0;

    public static MiscWorldSaveData Parse(string s, IFormatProvider? provider)
    {
        MiscWorldSaveData data = new();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<mwB>", "<mwA>"))
            ParseField(data, key, value);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out MiscWorldSaveData result)
    {
        // Vultu: probably make this better
        result = Parse(s, provider);
        return true;
    }

    public override string ToString()
    {
        return SerializeFields("<mwB>", "<mwA>");
    }
}
