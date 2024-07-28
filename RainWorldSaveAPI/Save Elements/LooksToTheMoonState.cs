using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI;

// TODO: Document fields
public class LooksToTheMoonState : IParsable<LooksToTheMoonState>
{
    public List<int> UnrecognizedIntegers { get; } = [];

    public List<bool> UnrecognizedBooleans { get; } = [];

    public List<string> UnrecognizedSaveStrings { get; } = [];

    public int PlayerEncounters { get; set; } = 0;

    public int PlayerEncountersWithMark { get; set; } = 0;

    public int NeuronsLeft { get; set; } = 0;

    public int NeuronGiftConversationCounter { get; set; } = 0;

    public int TotalNeuronsGiven { get; set; } = 0;

    public int InterruptionsFromLeaving { get; set; } = 0;

    public int InterruptionsFromBeingAnnoying { get; set; } = 0;

    public int TotalInterruptions { get; set; } = 0;

    public int TotalItemsBrought { get; set; } = 0;

    public List<string> DataPearlsRead { get; set; } = [];

    public List<string> MiscItemsDescribed { get; set; } = [];

    public List<string> ItemsTalkedAbout { get; set; } = [];

    /// <summary>
    /// Total number of colored pearls brought to Moon.
    /// </summary>
    public int TotalColoredPearlsBrought { get; set; } = 0;

    /// <summary>
    /// Total number of misc pearls brought to Moon (i.e. the ones that have random gibberish).
    /// </summary>
    public int TotalMiscPearlsBrought { get; set; } = 0;

    /// <summary>
    /// Unused. Set to -1 by default.
    /// </summary>
    public int ChatLogA { get; set; } = -1;

    /// <summary>
    /// Unused. Set to -1 by default.
    /// </summary>
    public int ChatLogB { get; set; } = -1;

    /// <summary>
    /// Whenver Moon has told the player to leave the neurons alone after grabbing one. <para/>
    /// Grabbing a neuron again after being warned will make Moon butthurt.
    /// </summary>
    public bool HasToldPlayerNotToEatNeurons { get; set; } = false;

    /// <summary>
    /// Player's reputation / like amount for Moon.
    /// </summary>
    public float PlayerReputation { get; set; } = 0f;

    /// <summary>
    /// Whenever Moon talked about Pebbles after Rivulet removed the Rarefaction Cell.
    /// </summary>
    public bool TalkedAboutPebblesDying { get; set; } = false;

    /// <summary>
    /// Whenever Moon has been given the Rarefaction Cell to describe it.
    /// </summary>
    public bool HasSeenRarefactionCell { get; set; } = false;

    public static LooksToTheMoonState Parse(string s, IFormatProvider? provider)
    {
        var state = new LooksToTheMoonState();

        state.UnrecognizedIntegers.Clear();
        state.UnrecognizedBooleans.Clear();
        state.UnrecognizedSaveStrings.Clear();

        var arrays = s.Split("<slosA>", StringSplitOptions.RemoveEmptyEntries);

        foreach (var array in arrays)
        {
            var elements = array.Split("<slosB>", StringSplitOptions.RemoveEmptyEntries);

            var key = elements[0];
            var value = elements.Length >= 2 ? elements[1] : "";

            if (key == "integersArray")
            {
                // Last slot is unused
                var integers = new int[14];

                state.UnrecognizedIntegers.AddRange(SaveUtils.LoadIntegerArray(value, ".", integers));

                state.PlayerEncounters = SaveUtils.ElementOrDefault(integers, 0, 0);
                state.PlayerEncountersWithMark = SaveUtils.ElementOrDefault(integers, 1, 0);
                state.NeuronsLeft = SaveUtils.ElementOrDefault(integers, 2, 0);
                state.NeuronGiftConversationCounter = SaveUtils.ElementOrDefault(integers, 3, 0);
                state.TotalNeuronsGiven = SaveUtils.ElementOrDefault(integers, 4, 0);
                state.InterruptionsFromLeaving = SaveUtils.ElementOrDefault(integers, 5, 0);
                state.InterruptionsFromBeingAnnoying = SaveUtils.ElementOrDefault(integers, 6, 0);
                state.TotalInterruptions = SaveUtils.ElementOrDefault(integers, 7, 0);
                state.TotalItemsBrought = SaveUtils.ElementOrDefault(integers, 8, 0);
                state.TotalColoredPearlsBrought = SaveUtils.ElementOrDefault(integers, 9, 0);
                state.TotalMiscPearlsBrought = SaveUtils.ElementOrDefault(integers, 10, 0);
                state.ChatLogA = SaveUtils.ElementOrDefault(integers, 11, 0);
                state.ChatLogB = SaveUtils.ElementOrDefault(integers, 12, 0);
            }
            else if (key == "miscBools")
            {
                var bools = new bool[1];

                state.UnrecognizedBooleans.AddRange(SaveUtils.LoadBooleanArray(value, bools));

                state.HasToldPlayerNotToEatNeurons = SaveUtils.ElementOrDefault(bools, 0, false);
            }
            else if (key == "significantPearls")
            {
                // TODO backwards compatible parsing here
                state.DataPearlsRead.AddRange(value.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
            else if (key == "miscItemsDescribed")
            {
                // TODO backwards compatible parsing here
                state.MiscItemsDescribed.AddRange(value.Split(",", StringSplitOptions.RemoveEmptyEntries));
            }
            else if (key == "likesPlayer")
            {
                state.PlayerReputation = float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
            }
            else if (key == "itemsAlreadyTalkedAbout")
            {
                state.ItemsTalkedAbout.AddRange(value.Split("<slosC>", StringSplitOptions.RemoveEmptyEntries));
            }
            else if (key == "talkedPebblesDeath")
            {
                state.TalkedAboutPebblesDying = true;
            }
            else if (key == "shownEnergyCell")
            {
                state.HasSeenRarefactionCell = true;
            }
            else if (array.Trim().Length > 0)
            {
                state.UnrecognizedSaveStrings.Add(array);
            }
        }

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LooksToTheMoonState result)
    {
        throw new NotImplementedException();
    }
}
