using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

// TODO: Document this
public class PlayerGuideState : SaveElementContainer, IRWSerializable<PlayerGuideState>
{
    [SaveField(0, "integersArray")]
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
    [SaveField(1, "itemTypes", ListDelimiter = ",", SerializeIfEmpty = true)]
    public List<string> FoodItemsLearned { get; set; } = [];

    // TODO: backwards compatibility
    [SaveField(2, "creatureTypes", ListDelimiter = ",", SerializeIfEmpty = true)]
    public List<string> CreatureTypesWarnedAbout { get; set; } = [];

    [SaveField(3, "likesPlayer")]
    public float PlayerReputation { get; set; } = 0;

    [SaveField(4, "directionHandHolding")]
    public float HandHolding { get; set; } = 0;

    [SaveField(5, "imagesShown", ListDelimiter = ".")]
    public List<string> ImagesShown { get; set; } = [];

    [SaveField(6, "forcedDirsGiven", ListDelimiter = ".")]
    public List<string> ForcedDirectionsGiven { get; set; } = [];

    public static PlayerGuideState Deserialize(string key, string[] values, SerializationContext? context)
    {
        PlayerGuideState data = new();

        data.DeserializeFields(values[0], "<pgsB>", "<pgsA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<pgsB>", "<pgsA>")
        ];

        return true;
    }

    protected override void DeserializeUnrecognizedField(string key, string[] values)
    {
        if (key.Trim() != "" && values.Length >= 1)
            UnrecognizedFields.Add((key, values));
    }
}
