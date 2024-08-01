using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

// TODO: Document fields
public class LooksToTheMoonState : SaveElementContainer, IParsable<LooksToTheMoonState>
{
    [SaveFileElement("integersArray")]
    public GenericIntegerArray Integers { get; set; } = new();

    public int PlayerEncounters
    {
        get => Integers.TryGet(0);
        set => Integers.TrySet(0, value);
    }

    public int PlayerEncountersWithMark
    {
        get => Integers.TryGet(1);
        set => Integers.TrySet(1, value);
    }

    public int NeuronsLeft
    {
        get => Integers.TryGet(2);
        set => Integers.TrySet(2, value);
    }

    public int NeuronGiftConversationCounter
    {
        get => Integers.TryGet(3);
        set => Integers.TrySet(3, value);
    }

    public int TotalNeuronsGiven
    {
        get => Integers.TryGet(4);
        set => Integers.TrySet(4, value);
    }

    public int InterruptionsFromLeaving
    {
        get => Integers.TryGet(5);
        set => Integers.TrySet(5, value);
    }

    public int InterruptionsFromBeingAnnoying
    {
        get => Integers.TryGet(6);
        set => Integers.TrySet(6, value);
    }

    public int TotalInterruptions
    {
        get => Integers.TryGet(7);
        set => Integers.TrySet(7, value);
    }

    public int TotalItemsBrought
    {
        get => Integers.TryGet(8);
        set => Integers.TrySet(8, value);
    }

    /// <summary>
    /// Total number of colored pearls brought to Moon.
    /// </summary>
    public int TotalColoredPearlsBrought
    {
        get => Integers.TryGet(9);
        set => Integers.TrySet(9, value);
    }

    /// <summary>
    /// Total number of misc pearls brought to Moon (i.e. the ones that have random gibberish).
    /// </summary>
    public int TotalMiscPearlsBrought
    {
        get => Integers.TryGet(10);
        set => Integers.TrySet(10, value);
    }

    /// <summary>
    /// Unused. Set to -1 by default.
    /// </summary>
    public int ChatLogA
    {
        get => Integers.TryGet(11, -1);
        set => Integers.TrySet(11, value);
    }

    /// <summary>
    /// Unused. Set to -1 by default.
    /// </summary>
    public int ChatLogB
    {
        get => Integers.TryGet(12, -1);
        set => Integers.TrySet(12, value);
    }

    [SaveFileElement("miscBools")]
    public GenericBoolArray Booleans { get; set; } = new();

    /// <summary>
    /// Whenver Moon has told the player to leave the neurons alone after grabbing one. <para/>
    /// Grabbing a neuron again after being warned will make Moon butthurt.
    /// </summary>
    public bool HasToldPlayerNotToEatNeurons
    {
        get => Booleans.TryGet(0);
        set => Booleans.TrySet(0, value);
    }

    [SaveFileElement("significantPearls", ListDelimiter = ",")]
    public List<string> DataPearlsRead { get; set; } = [];

    [SaveFileElement("miscItemsDescribed", ListDelimiter = ",")]
    public List<string> MiscItemsDescribed { get; set; } = [];

    /// <summary>
    /// Player's reputation / like amount for Moon.
    /// </summary>
    [SaveFileElement("likesPlayer")]
    public float PlayerReputation { get; set; } = 0f;

    [SaveFileElement("itemsAlreadyTalkedAbout", ListDelimiter = "<slosC>")]
    public List<string> ItemsTalkedAbout { get; set; } = [];

    /// <summary>
    /// Whenever Moon talked about Pebbles after Rivulet removed the Rarefaction Cell.
    /// </summary>
    [SaveFileElement("talkedPebblesDeath", true)]
    public bool TalkedAboutPebblesDying { get; set; } = false;

    /// <summary>
    /// Whenever Moon has been given the Rarefaction Cell to describe it.
    /// </summary>
    [SaveFileElement("shownEnergyCell", true)]
    public bool HasSeenRarefactionCell { get; set; } = false;

    public static LooksToTheMoonState Parse(string s, IFormatProvider? provider)
    {
        var state = new LooksToTheMoonState();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<slosB>", "<slosA>"))
            ParseField(state, key, value);

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LooksToTheMoonState result)
    {
        throw new NotImplementedException();
    }
}
