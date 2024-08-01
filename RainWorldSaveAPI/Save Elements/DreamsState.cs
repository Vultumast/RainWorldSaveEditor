using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

// TODO: Document fields
public class DreamsState : SaveElementContainer, IParsable<DreamsState>
{
    [SaveFileElement("integersArray")]
    public GenericIntegerArray Integers { get; set; } = new();

    public int CyclesSinceLastDream
    {
        get => Integers.TryGet(0);
        set => Integers.TrySet(0, value);
    }

    public int CyclesSinceLastFamilyDream
    {
        get => Integers.TryGet(1);
        set => Integers.TrySet(1, value);
    }

    public int CyclesSinceLastGuideDream
    {
        get => Integers.TryGet(2);
        set => Integers.TrySet(2, value);
    }

    public int FamilyThread
    {
        get => Integers.TryGet(3);
        set => Integers.TrySet(3, value);
    }

    public int GuideThread
    {
        get => Integers.TryGet(4);
        set => Integers.TrySet(4, value);
    }

    public int InGWOrSHCounter
    {
        get => Integers.TryGet(5);
        set => Integers.TrySet(5, value);
    }

    public bool EverSleptInSB
    {
        get => Integers.TryGet(6) == 1;
        set => Integers.TrySet(6, value ? 1 : 0);
    }

    public bool EverSleptInSB_S01
    {
        get => Integers.TryGet(7) == 1;
        set => Integers.TrySet(7, value ? 1 : 0);
    }

    public bool EverAteMoonNeuron
    {
        get => Integers.TryGet(8) == 1;
        set => Integers.TrySet(8, value ? 1 : 0);
    }

    public static DreamsState Parse(string s, IFormatProvider? provider)
    {
        var state = new DreamsState();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<dsB>", "<dsA>"))
            ParseField(state, key, value);

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DreamsState result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return SerializeFields("<dsB>", "<dsA>");
    }
}