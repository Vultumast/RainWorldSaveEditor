using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Booleans = {ToString()}")]
public class GenericBoolArray : IParsable<GenericBoolArray>
{
    public bool[] Booleans { get; set; } = [];

    public bool TryGet(int index, bool fallback = false)
    {
        if (0 <= index && index < Booleans.Length)
            return Booleans[index];

        return fallback;
    }

    public void TrySet(int index, bool value)
    {
        if (0 <= index && index < Booleans.Length)
            Booleans[index] = value;
    }

    public static GenericBoolArray Parse(string s, IFormatProvider? provider)
    {
        var array = new GenericBoolArray();

        array.Booleans = s.Select(x => x == '1').ToArray();

        return array;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GenericBoolArray result)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return string.Concat(Booleans.Select(x => x ? '1' : '0'));
    }
}
