using RainWorldSaveAPI.Base;
using RainWorldSaveAPI.Save_Elements;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

public class MiscWorldSaveData : SaveElementContainer, IRWSerializable<MiscWorldSaveData>
{
    /// <summary>
    /// Tracks the number of times the player has talked with Five Pebbles.
    /// </summary>
    [SaveField(0, "SSaiConversationsHad")]
    public IntSerializeIfNotZero TimesTalkedWithFivePebbles { get; set; } = new();

    /// <summary>
    /// Tracks the number of times the player has been thrown out by Five Pebbles. <para/>
    /// For most slugcats, FP becomes hostile after throwing them out once and will kill them on subsequent visits.
    /// </summary>
    [SaveField(1, "SSaiThrowOuts")]
    public IntSerializeIfNotZero TimesKickedOutByFivePebbles { get; set; } = new();

    /// <summary>
    /// Looks to the Moon's current state.
    /// </summary>
    [SaveField(2, "SLaiState")]
    public LooksToTheMoonState? LooksToTheMoonState { get; set; } = null;

    /// <summary>
    /// The guiding overseer's current state.
    /// </summary>
    [SaveField(3, "playerGuideState")]
    public PlayerGuideState PlayerGuideState { get; set; } = new();

    /// <summary>
    /// Tracks whenever Hunter successfully delivered the Green Neuron to Looks to the Moon and revived her.
    /// </summary>
    [SaveField(4, "MOONREVIVED")]
    public bool HasRevivedLooksToTheMoon { get; set; } = false;

    /// <summary>
    /// Tracks whenever Hunter visited Five Pebbles while having the Green Neuron.
    /// </summary>
    [SaveField(5, "PEBBLESHELPED")]
    public bool HasFivePebblesSeenGreenNeuron { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player visited Five Pebbles via the memory arrays entrance instead of the utility shaft. <para/>
    /// This flag is set specifically if the player enters room SS_D02.
    /// </summary>
    [SaveField(6, "MEMORYFROLICK")]
    public bool HasFrolickedInMemoryArrays { get; set; } = false;

    /// <summary>
    /// Tracks the number of cycles passed since first conversation with Five Pebbles. <para/>
    /// Seems to be used for tracking Spearmaster's pearl wound recovery.
    /// </summary>
    [SaveField(7, "CyclesSinceSSai")]
    public IntSerializeIfNotZero CyclesSinceFirstFivePebblesVisit { get; set; } = new();

    /// <summary>
    /// Tracks whenever Rivulet removed the Rarefaction Cell from Five Pebbles's generator.
    /// </summary>
    [SaveField(8, "ENERGYRAILOFF")]
    public bool HasRemovedRarefactionCell { get; set; } = false;

    /// <summary>
    /// Tracks Five Pebbles's conversation state related to the Rarefaction Cell. <para/>
    /// 0 = Default state <para/>
    /// 1 = Pebbles told Rivulet about the cell on their first visit <para/>
    /// 2 = Pebbles acknowledged that the cell has been removed on Rivulet's first visit (without them holding the cell) <para/>
    /// 3 = Pebbles saw Rivulet hold the cell on one of the visits, or Pebbles acknowledged that the cell has been removed after being visited again
    /// </summary>
    [SaveField(9, "EnergySeenState")]
    public IntSerializeIfNotZero RarefactionCellConversationState { get; set; } = new();

    /// <summary>
    /// Tracks whenever Rivulet delivered the Rarefaction Cell to Moon's core.
    /// </summary>
    [SaveField(10, "MOONHEART")]
    public bool DeliveredRarefactionCellToMoon { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has given Moon the Cloak.
    /// </summary>
    [SaveField(11, "MOONROBE")]
    public bool HasGivenMoonTheCloak { get; set; } = false;

    /// <summary>
    /// Tracks whenever Moon has encoded Spearmaster's pearl with a new message.
    /// </summary>
    [SaveField(12, "SMPEARLTAGGED")]
    public bool HasMoonEncodedSpearmasterPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever Five Pebbles has talked about his music pearl.
    /// </summary>
    [SaveField(13, "HALCYONTALK")]
    public bool HasDiscussedFivePebblesMusicPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has stolen Five Pebbles's music pearl. <para/>
    /// Truly despicable behaviour indeed.
    /// </summary>
    [SaveField(14, "HALCYONSTOLE")]
    public bool HasStolenFivePebblesMusicPearl { get; set; } = false;

    /// <summary>
    /// Tracks whenever the player has talked with Five Pebbles after delivering the Rarefaction Cell to Moon.
    /// </summary>
    [SaveField(15, "PEBRIVPOST")]
    public bool HasTalkedWithFivePebblesInRivuletPostgame { get; set; } = false;

    /// <summary>
    /// Tracks whenever Saint has talked with Five Pebbles and / or Looks to the Moon in Rubicon's final room.
    /// </summary>
    [SaveField(16, "HRMELT")]
    public bool TalkedWithOraclesInRubicon { get; set; } = false;

    /// <summary>
    /// Tracks how many cycles have passed since a Slugpup has spawned. <para/>
    /// This is used to guarantee Slugpup spawns after 25 cycles (5 for Hunter).
    /// </summary>
    [SaveField(17, "CyclesSinceSlugpup")]
    public IntSerializeIfNotZero CyclesSinceLastSlugpupSpawn { get; set; } = new();

    public static MiscWorldSaveData Deserialize(string key, string[] values, SerializationContext? context)
    {
        MiscWorldSaveData data = new();

        data.DeserializeFields(values[0], "<mwB>", "<mwA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<mwB>", "<mwA>")
        ];

        return values[0] != ""; // Seems to be skipped if there's no information.
    }

    protected override void DeserializeUnrecognizedField(string key, string[] values)
    {
        if (key.Trim() != "")
            UnrecognizedFields.Add((key, values));
    }
}
