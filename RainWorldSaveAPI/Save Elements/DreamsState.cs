using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;

public class DreamsState
{
    public int CyclesSinceLastDream { get; set; } = 0;

    public int CyclesSinceLastFamilyDream { get; set; } = 0;

    public int CyclesSinceLastGuideDream { get; set; } = 0;

    public int FamilyThread { get; set; } = 0;

    public int GuideThread { get; set; } = 0;

    public int InGWOrSHCounter { get; set; } = 0;

    public bool EverSleptInSB { get; set; } = false;

    public bool EverSleptInSB_S01 { get; set; } = false;

    public bool EverAteMoonNeuron { get; set; } = false;

    public List<int> UnrecognizedIntegers { get; } = [];

    public List<string> UnrecognizedSaveStrings { get; } = [];

    public void Read(string value)
    {
        UnrecognizedIntegers.Clear();
        UnrecognizedSaveStrings.Clear();

        var arrays = value.Split("<dsA>");

        foreach (var array in arrays)
        {
            var elements = array.Split("<dsB>");

            if (elements.Length != 2)
            {
                // TODO Better handling
                Logger.Debug("A dreamstate array has less than 2 elements.");
                continue;
            }

            var key = elements[0];
            var arr = elements[1];

            if (key == "integersArray")
            {
                var integers = arr.Split('.').Select(x => int.Parse(x, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)).ToArray();

                // TODO rewrite this?
                CyclesSinceLastDream = integers.Length >= 1 ? integers[0] : 0;
                CyclesSinceLastFamilyDream = integers.Length >= 2 ? integers[1] : 0;
                CyclesSinceLastGuideDream = integers.Length >= 3 ? integers[2] : 0;
                FamilyThread = integers.Length >= 4 ? integers[3] : 0;
                GuideThread = integers.Length >= 5 ? integers[4] : 0;
                InGWOrSHCounter = integers.Length >= 6 ? integers[5] : 0;
                EverSleptInSB = integers.Length >= 7 && integers[6] == 1;
                EverSleptInSB_S01 = integers.Length >= 8 && integers[7] == 1;
                EverAteMoonNeuron = integers.Length >= 9 && integers[8] == 1;

                if (integers.Length >= 10)
                {
                    UnrecognizedIntegers.AddRange(integers.Skip(9));
                }
            }
            else if (array.Trim().Length > 0)
            {
                UnrecognizedSaveStrings.Add(array);
            }
        }
    }

    public string Write()
    {
        throw new NotImplementedException();
    }
}