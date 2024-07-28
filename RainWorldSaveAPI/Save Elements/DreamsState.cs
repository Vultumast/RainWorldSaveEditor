using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

// TODO: Document fields
public class DreamsState : IParsable<DreamsState>
{
    public List<int> UnrecognizedIntegers { get; } = [];

    public List<string> UnrecognizedSaveStrings { get; } = [];

    public int CyclesSinceLastDream { get; set; } = 0;

    public int CyclesSinceLastFamilyDream { get; set; } = 0;

    public int CyclesSinceLastGuideDream { get; set; } = 0;

    public int FamilyThread { get; set; } = 0;

    public int GuideThread { get; set; } = 0;

    public int InGWOrSHCounter { get; set; } = 0;

    public bool EverSleptInSB { get; set; } = false;

    public bool EverSleptInSB_S01 { get; set; } = false;

    public bool EverAteMoonNeuron { get; set; } = false;

    public static DreamsState Parse(string s, IFormatProvider? provider)
    {
        var state = new DreamsState();

        state.UnrecognizedIntegers.Clear();
        state.UnrecognizedSaveStrings.Clear();

        var arrays = s.Split("<dsA>", StringSplitOptions.RemoveEmptyEntries);

        foreach (var array in arrays)
        {
            var elements = array.Split("<dsB>", StringSplitOptions.RemoveEmptyEntries);

            var key = elements[0];
            var arr = elements.Length >= 2 ? elements[1] : "";

            if (key == "integersArray")
            {
                var integers = new int[9];

                state.UnrecognizedIntegers.AddRange(SaveUtils.LoadIntegerArray(arr, ".", integers));

                state.CyclesSinceLastDream = SaveUtils.ElementOrDefault(integers, 0, 0);
                state.CyclesSinceLastFamilyDream = SaveUtils.ElementOrDefault(integers, 1, 0);
                state.CyclesSinceLastGuideDream = SaveUtils.ElementOrDefault(integers, 2, 0);
                state.FamilyThread = SaveUtils.ElementOrDefault(integers, 3, 0);
                state.GuideThread = SaveUtils.ElementOrDefault(integers, 4, 0);
                state.InGWOrSHCounter = SaveUtils.ElementOrDefault(integers, 5, 0);
                state.EverSleptInSB = SaveUtils.ElementOrDefault(integers, 6, 0) == 1;
                state.EverSleptInSB_S01 = SaveUtils.ElementOrDefault(integers, 7, 0) == 1;
                state.EverAteMoonNeuron = SaveUtils.ElementOrDefault(integers, 8, 0) == 1;
            }
            else if (array.Trim().Length > 0)
            {
                state.UnrecognizedSaveStrings.Add(array);
            }
        }

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DreamsState result)
    {
        throw new NotImplementedException();
    }

    public string Write()
    {
        throw new NotImplementedException();
    }
}