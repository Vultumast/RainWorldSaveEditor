using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainWorldSaveEditor.Save;

public static class SaveUtils
{
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
                Logger.Log($"Failed to read an entry.");
            }
        }
    }
}
