using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RainWorldSaveAPI.SaveElements;

public class RegionState : SaveElementContainer, IRWSerializable<RegionState>
{
    [SaveField(0, "REGIONNAME")]
    public string RegionName { get; set; } = "";

    [SaveField(1, "LASTCYCLEUPDATED")]
    public int LastCycleUpdated { get; set; } = 0;

    // TODO backwards compatibility
    [SaveField(2, "SWARMROOMS", ListDelimiter = ",", SerializeIfEmpty = true)]
    public List<string> SwarmRooms { get; set; } = [];

    // TODO backwards compatibility
    [SaveField(3, "LINEAGES", ListDelimiter = ",")]
    public List<string> Lineages { get; set; } = [];

    [SaveField(4, "OBJECTS", ListDelimiter = "<rgC>", TrailingListDelimiter = true)]
    public List<string> Objects { get; set; } = [];

    [SaveField(5, "POPULATION", ListDelimiter = "<rgC>", TrailingListDelimiter = true)]
    public List<string> Population { get; set; } = [];

    [SaveField(6, "STICKS", ListDelimiter = "<rgC>", TrailingListDelimiter = true)]
    public List<string> SavedSticks { get; set; } = [];

    [SaveField(7, "CONSUMEDITEMS", ListDelimiter = "<rgC>")]
    public List<string> ConsumedItems { get; set; } = [];

    // TODO backwards compatibility
    [SaveField(8, "ROOMSVISITED", ListDelimiter = ",", SerializeIfEmpty = true)]
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
