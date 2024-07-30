using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveAPI;

public static class SaveUtils
{
    public static MethodInfo? GetParseMethod(this Type type)
    {
        MethodInfo parseMethodInfo = null!;
        // Vultu: Get method ``Parse(string s, IFormatProvider? provider)``
        foreach (var method in type.GetMethods())
        {
            var parameters = method.GetParameters();
            if (method.Name == "Parse" && parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(IFormatProvider))
            {
                parseMethodInfo = method;
                break;
            }
        }

        if (parseMethodInfo == null)
        {

        }
        return parseMethodInfo;
    }

    public static IEnumerable<(string Key, string Value)> GetFields(string data, string valueDelimiter, string entryDelimiter)
    {
        string[] entries = data.Split(entryDelimiter, StringSplitOptions.RemoveEmptyEntries);

        foreach (var entry in entries)
        {
            string[] fields = entry.Split(valueDelimiter, 2);

            if (fields.Length == 2)
            {
                yield return (fields[0], fields[1]);
            }
            else if (fields.Length == 1)
            {
                yield return (fields[0], "");
            }
            else
            {
                Logger.Error($"Failed to read an entry.");
            }
        }
    }

    [Obsolete("See GenericIntegerArray")]
    public static int[] LoadIntegerArray(string value, string delimiter, int[] integers)
    {
        var parsedIntegers = value.Split(delimiter).Select(x => int.Parse(x, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture)).ToArray();

        for (int i = 0; i < Math.Min(integers.Length, parsedIntegers.Length); i++)
        {
            integers[i] = parsedIntegers[i];
        }

        if (parsedIntegers.Length > integers.Length)
        {
            // Return unrecognized integers
            return parsedIntegers[integers.Length..];
        }

        return [];
    }

    public static bool[] LoadBooleanArray(string value, bool[] bools)
    {
        var parsedBools = value.Select(x => x == '1').ToArray();

        for (int i = 0; i < Math.Min(bools.Length, parsedBools.Length); i++)
        {
            bools[i] = parsedBools[i];
        }

        if (parsedBools.Length > bools.Length)
        {
            // Return unrecognized booleans
            return parsedBools[bools.Length..];
        }

        return [];
    }

    public static T ElementOrDefault<T>(T[] arr, int index, T defaultValue) => 0 <= index && index < arr.Length ? arr[index] : defaultValue;
}
