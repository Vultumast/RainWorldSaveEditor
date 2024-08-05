using RainWorldSaveAPI.Base;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Integers = {string.Join(\", \", Integers)}")]
public class GenericIntegerArray : IRWSerializable<GenericIntegerArray>
{
    public int[] Integers { get; set; } = [];

    // TODO this should be set via an attribute on the property
    public char DelimiterUsed { get; private set; } = ' ';

    public int TryGet(int index, int fallback = 0)
    {
        if (0 <= index && index < Integers.Length)
            return Integers[index];

        return fallback;
    }

    public void TrySet(int index, int value)
    {
        if (0 <= index && index < Integers.Length)
            Integers[index] = value;
    }

    public static GenericIntegerArray Deserialize(string key, string[] values, SerializationContext? context)
    {
        var array = new GenericIntegerArray();

        char delimiter;

        if (values[0].Contains('.') && !values[0].Contains(','))
            delimiter = '.';

        else if (values[0].Contains(',') && !values[0].Contains('.'))
            delimiter = ',';

        else throw new ArgumentException("Cannot determine integer array divider.");

        array.DelimiterUsed = delimiter;

        array.Integers = values[0].Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)).ToArray();

        return array;
    }

    public bool Serialize(out string? key, out string[] values, SerializationContext? context)
    {
        if (DelimiterUsed == ' ')
            throw new InvalidOperationException("GenericIntegerArray Delimiter was not set, cannot deserialize!");

        key = null;
        values = [
            string.Join(DelimiterUsed, Integers)
        ];

        return true;
    }
}
