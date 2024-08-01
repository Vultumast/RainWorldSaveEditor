using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Integers = {string.Join(\", \", Integers)}")]
public class GenericIntegerArray : IParsable<GenericIntegerArray>
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

    public static GenericIntegerArray Parse(string s, IFormatProvider? provider)
    {
        var array = new GenericIntegerArray();

        char delimiter;

        if (s.Contains('.') && !s.Contains(','))
            delimiter = '.';

        else if (s.Contains(',') && !s.Contains('.'))
            delimiter = ',';

        else throw new ArgumentException("Cannot determine integer array divider.");

        array.DelimiterUsed = delimiter;

        array.Integers = s.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x, NumberStyles.Any, CultureInfo.InvariantCulture)).ToArray();

        return array;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GenericIntegerArray result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        if (DelimiterUsed == ' ')
            throw new InvalidOperationException("GenericIntegerArray Delimiter was not set, cannot deserialize!");

        return string.Join(DelimiterUsed, Integers);
    }
}
