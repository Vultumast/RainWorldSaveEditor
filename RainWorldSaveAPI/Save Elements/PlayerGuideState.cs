using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI;

// TODO: Document this
public class PlayerGuideState : IParsable<PlayerGuideState>
{
    public List<string> UnrecognizedSaveStrings { get; } = [];

    public bool PlayerHasVisitedMoon { get; set; } = false;

    public int SuperJumpsShown { get; set; } = 0;

    public int PickupObjectsShown { get; set; } = 0;

    public bool ScavengerTradeInstructionCompleted { get; set; } = false;

    public bool AngryWithPlayer { get; set; } = false;

    public bool DisplayedAnger { get; set; } = false;

    public int GuideSymbol { get; set; } = 0;

    public List<string> FoodItemsLearned { get; set; } = [];

    public List<string> CreatureTypesWarnedAbout { get; set; } = [];

    public float PlayerReputation { get; set; } = 0;

    public float HandHolding { get; set; } = 0;

    public List<string> ImagesShown { get; set; } = [];

    public List<string> ForcedDirectionsGiven { get; set; } = [];

    public static PlayerGuideState Parse(string s, IFormatProvider? provider)
    {
        var state = new PlayerGuideState();

        state.UnrecognizedSaveStrings.Clear();

        var arrays = s.Split("<pgsA>", StringSplitOptions.RemoveEmptyEntries);

        foreach (var array in arrays)
        {
            var elements = array.Split("<pgsB>", StringSplitOptions.RemoveEmptyEntries);

            var key = elements[0];
            var value = elements.Length >= 2 ? elements[1] : "";

            if (key == "integersArray")
            {
                var integers = new int[7];

                _ = SaveUtils.LoadIntegerArray(value, ".", integers);

                state.PlayerHasVisitedMoon = SaveUtils.ElementOrDefault(integers, 0, 0) > 0;
                state.SuperJumpsShown = SaveUtils.ElementOrDefault(integers, 1, 0);
                state.PickupObjectsShown = SaveUtils.ElementOrDefault(integers, 2, 0);
                state.ScavengerTradeInstructionCompleted = SaveUtils.ElementOrDefault(integers, 3, 0) > 0;
                state.AngryWithPlayer = SaveUtils.ElementOrDefault(integers, 4, 0) > 0;
                state.DisplayedAnger = SaveUtils.ElementOrDefault(integers, 5, 0) > 0;
                state.GuideSymbol = SaveUtils.ElementOrDefault(integers, 6, 0);
            }
            else if (key == "itemTypes")
            {
                // TODO: backwards compatibility
                state.FoodItemsLearned.AddRange(value.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
            else if (key == "creatureTypes")
            {
                // TODO: backwards compatibility
                state.CreatureTypesWarnedAbout.AddRange(value.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
            else if (key == "likesPlayer")
            {
                state.PlayerReputation = float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            else if (key == "directionHandHolding")
            {
                state.HandHolding = float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            else if (key == "imagesShown")
            {
                state.ImagesShown.AddRange(value.Split("."));
            }
            else if (key == "forcedDirsGiven")
            {
                state.ForcedDirectionsGiven.AddRange(value.Split("."));
            }
            else if (array.Trim().Length > 0)
            {
                state.UnrecognizedSaveStrings.Add(array);
            }
        }

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PlayerGuideState result)
    {
        throw new NotImplementedException();
    }
}
