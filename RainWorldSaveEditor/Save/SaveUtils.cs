using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Save;

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
}
