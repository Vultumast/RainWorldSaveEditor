using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Slugcat = {Slugcat} | ColorsEnabled = {ColorsEnabled} | Colors = {string.Join(\", \", ColorChoices)}")]
public class ColorChoice : IRWSerializable<ColorChoice>
{
    public string Slugcat { get; set; } = "White";

    public bool ColorsEnabled { get; set; } = false;

    public List<string> ColorChoices { get; set; } = [];

    public static ColorChoice Deserialize(string key, string[] values, SerializationContext? context)
    {
        return new ColorChoice
        {
            Slugcat = values[0],
            ColorsEnabled = values[1] == "1",
            ColorChoices = new(values.Length <= 2 ? [] : values[2].Split("<mpdC>", StringSplitOptions.RemoveEmptyEntries))
        };
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        key = null;
        values = [
            Slugcat,
            ColorsEnabled ? "1" : "0",
            string.Join("<mpdC>", ColorChoices)
        ];

        return true;
    }
}
