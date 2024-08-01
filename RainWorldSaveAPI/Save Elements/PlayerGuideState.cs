using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

// TODO: Document this
public class PlayerGuideState : SaveElementContainer, IParsable<PlayerGuideState>
{
    [SaveFileElement("integersArray")]
    public GenericIntegerArray Integers { get; set; } = new();

    public bool PlayerHasVisitedMoon
    {
        get => Integers.TryGet(0) == 1;
        set => Integers.TrySet(0, value ? 1 : 0);
    }

    public int SuperJumpsShown
    {
        get => Integers.TryGet(1);
        set => Integers.TrySet(1, value);
    }

    public int PickupObjectsShown
    {
        get => Integers.TryGet(2);
        set => Integers.TrySet(2, value);
    }

    public bool ScavengerTradeInstructionCompleted
    {
        get => Integers.TryGet(3) > 0;
        set => Integers.TrySet(3, value ? 1 : 0);
    }

    public bool AngryWithPlayer
    {
        get => Integers.TryGet(4) > 0;
        set => Integers.TrySet(4, value ? 1 : 0);
    }

    public bool DisplayedAnger
    {
        get => Integers.TryGet(5) > 0;
        set => Integers.TrySet(5, value ? 1 : 0);
    }

    public int GuideSymbol
    {
        get => Integers.TryGet(6);
        set => Integers.TrySet(6, value);
    }

    // TODO: backwards compatibility
    [SaveFileElement("itemTypes", ListDelimiter = ",")]
    public List<string> FoodItemsLearned { get; set; } = [];

    // TODO: backwards compatibility
    [SaveFileElement("creatureTypes", ListDelimiter = ",")]
    public List<string> CreatureTypesWarnedAbout { get; set; } = [];

    [SaveFileElement("likesPlayer")]
    public float PlayerReputation { get; set; } = 0;

    [SaveFileElement("directionHandHolding")]
    public float HandHolding { get; set; } = 0;

    [SaveFileElement("imagesShown", ListDelimiter = ".")]
    public List<string> ImagesShown { get; set; } = [];

    [SaveFileElement("forcedDirsGiven", ListDelimiter = ".")]
    public List<string> ForcedDirectionsGiven { get; set; } = [];

    public static PlayerGuideState Parse(string s, IFormatProvider? provider)
    {
        PlayerGuideState data = new();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<pgsB>", "<pgsA>"))
            ParseField(data, key, value);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PlayerGuideState result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return SerializeFields("<pgsB>", "<pgsA>");
    }
}
