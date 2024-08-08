using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI.SaveElements;

public class RegionState : SaveElementContainer, IRWSerializable<RegionState>
{
    [SaveFileElement("REGIONNAME", Order = 0)]
    public string RegionName { get; set; } = "";

    [SaveFileElement("LASTCYCLEUPDATED", Order = 1)]
    public int LastCycleUpdated { get; set; } = 0;

    // TODO backwards compatibility
    [SaveFileElement("SWARMROOMS", ListDelimiter = ",", SerializeIfEmpty = true, Order = 2)]
    public List<string> SwarmRooms { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("LINEAGES", ListDelimiter = ",", Order = 3)]
    public List<string> Lineages { get; set; } = [];

    [SaveFileElement("OBJECTS", ListDelimiter = "<rgC>", TrailingListDelimiter = true,  Order = 4)]
    public List<string> Objects { get; set; } = [];

    [SaveFileElement("POPULATION", ListDelimiter = "<rgC>", TrailingListDelimiter = true, Order = 5)]
    public List<string> Population { get; set; } = [];

    [SaveFileElement("STICKS", ListDelimiter = "<rgC>", TrailingListDelimiter = true, Order = 6)]
    public List<string> SavedSticks { get; set; } = [];

    [SaveFileElement("CONSUMEDITEMS", ListDelimiter = "<rgC>", Order = 7)]
    public List<string> ConsumedItems { get; set; } = [];

    // TODO backwards compatibility
    [SaveFileElement("ROOMSVISITED", ListDelimiter = ",", SerializeIfEmpty = true, Order = 8)]
    public List<string> RoomsVisited { get; set; } = [];

    public static RegionState Deserialize(string key, string[] values, SerializationContext? context)
    {
        RegionState data = new();

        data.DeserializeFields(values[0], "<rgB>", "<rgA>");

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            SerializeFields("<rgB>", "<rgA>")
        ];

        return true;
    }

    protected override void DeserializeUnrecognizedField(string key, string[] values)
    {
        if (key.Trim() != "" && values.Length >= 1)
            UnrecognizedFields.Add((key, [values[0]]));
    }
}
