using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI.SaveElements;

public class RegionState : SaveElementContainer, IParsable<RegionState>
{
    [SaveFileElement("REGIONNAME", Order = 0)]
    public string RegionName { get; set; } = "";

    [SaveFileElement("LASTCYCLEUPDATED", Order = 1)]
    public int LastCycleUpdated { get; set; } = 0;

    // TODO backwards compatibility
    [SaveFileElement("SWARMROOMS", ListDelimiter = ",", Order = 2)]
    public List<string> SwarmRooms { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("LINEAGES", ListDelimiter = ",", Order = 3)]
    public List<string> Lineages { get; set; } = [];

    [SaveFileElement("OBJECTS", ListDelimiter = "<rgC>", Order = 4)]
    public List<string> Objects { get; set; } = [];

    [SaveFileElement("POPULATION", ListDelimiter = "<rgC>", Order = 5)]
    public List<string> Population { get; set; } = [];

    [SaveFileElement("STICKS", ListDelimiter = "<rgC>", Order = 6)]
    public List<string> SavedSticks { get; set; } = [];

    [SaveFileElement("CONSUMEDITEMS", ListDelimiter = "<rgC>", Order = 7)]
    public List<string> ConsumedItems { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("ROOMSVISITED", ListDelimiter = ",", Order = 8)]
    public List<string> RoomsVisited { get; set; } = [];

    public static RegionState Parse(string s, IFormatProvider? provider)
    {
        var state = new RegionState();

        foreach ((var key, var value) in SaveUtils.GetFields(s, "<rgB>", "<rgA>"))
            ParseField(state, key, value);

        return state;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RegionState result)
    {
        throw new NotImplementedException();
    }
}
