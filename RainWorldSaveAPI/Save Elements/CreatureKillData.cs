using RainWorldSaveAPI.Base;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI;

public class CreatureKillData : IRWSerializable<CreatureKillData>
{
    public string Creature { get; set; } = "";

    public int Kills { get; set; } = 0;

    public static CreatureKillData Deserialize(string key, string[] values, SerializationContext? context)
    {
        var parts = values[0].Split("<svD>");

        var data = new CreatureKillData
        {
            Creature = parts[0],
            Kills = int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture)
        };

        return data;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            $"{Creature}<svD>{Kills}"
        ];

        return true;
    }
}
