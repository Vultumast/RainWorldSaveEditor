using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI.SaveElements;

public class RegionState : SaveElementContainer, IParsable<RegionState>
{
    [SaveFileElement("REGIONNAME")]
    public string RegionName { get; set; } = "";

    [SaveFileElement("LASTCYCLEUPDATED")]
    public int LastCycleUpdated { get; set; } = 0;

    // TODO backwards compatibility
    [SaveFileElement("SWARMROOMS", ListDelimiter = ",")]
    public List<string> SwarmRooms { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("LINEAGES", ListDelimiter = ",")]
    public List<string> Lineages { get; set; } = [];

    [SaveFileElement("OBJECTS", ListDelimiter = "<rgC>")]
    public List<string> Objects { get; set; } = [];

    [SaveFileElement("POPULATION", ListDelimiter = "<rgC>")]
    public List<string> Population { get; set; } = [];

    [SaveFileElement("STICKS", ListDelimiter = "<rgC>")]
    public List<string> SavedSticks { get; set; } = [];

    [SaveFileElement("CONSUMEDITEMS", ListDelimiter = "<rgC>")]
    public List<string> ConsumedItems { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("ROOMSVISITED", ListDelimiter = ",")]
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
