using RainWorldSaveAPI;
using System.Text;
using System.Text.RegularExpressions;

namespace SerializationTest;

/*
 * Put any save files you want to check under the "saves" folder created by the console app.
 * Preferably the save files shouldn't have any extensions, i.e. "sav" or "sav_foobar" should be fine.
 * Generated results for each file will be under the "results" folder, where each save file will have a folder with a matching name.
*/

internal static partial class Program
{
    const string LoadPath = "saves";
    const string WritePath = "results";

    static void Main(string[] args)
    {
        using var apiLoggerStream = new StreamWriter(File.OpenWrite("apilog.txt"));
        Logger.LogStreamWriter = apiLoggerStream;
        Logger.LogToConsole = false;

        Directory.CreateDirectory(LoadPath);
        Directory.CreateDirectory(WritePath);

        var files = Directory.GetFiles(LoadPath);

        Console.WriteLine($"Found {files.Length} files under \"{LoadPath}\".");

        if (files.Length == 0)
        {
            Console.WriteLine($"No saves found, put the saves you want to test under \"{LoadPath}\".");
            return;
        }

        foreach (var file in files.Select(Path.GetFileName).Where(x => x != null).Cast<string>())
        {
            RunSerializationTest(file);
        }
    }

    private static void RunSerializationTest(string file)
    {
        Console.WriteLine($"Checking file {file}...");

        try
        {
            var inputPath = Path.Combine(LoadPath, file);
            var resultsPath = Path.Combine(WritePath, file);

            Directory.CreateDirectory(resultsPath);

            GetSaveOriginalAndParsed(inputPath, out var original, out var parsed);
            WriteComparisons(resultsPath, original, parsed);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an exception while checking file {file}!");
            Console.WriteLine($"{e}");
        }
    }

    private static void GetSaveOriginalAndParsed(string path, out string original, out string parsed)
    {
        using var fs = File.OpenRead(path);

        var table = HashtableSerializer.Read(fs);
        var save = new RainWorldSave();

        if (table["save"] is not string saveData)
            throw new InvalidOperationException("Expected save hashtable to have a \"save\" field.");

        save.Read(saveData);
        var serializedData = save.Write();

        original = saveData;
        parsed = serializedData;
    }

    [GeneratedRegex("SAV STATE NUMBER(?:\r|\n|\r\n)<svB>(?:\r|\n|\r\n)([^<\r\n]*)(?:\r|\n|\r\n)<svA>")]
    private static partial Regex SaveStateSectionRegex();

    private static void WriteComparisons(string resultsPath, string original, string parsed)
    {
        File.WriteAllText(Path.Combine(resultsPath, "original.txt"), original);
        File.WriteAllText(Path.Combine(resultsPath, "parsed.txt"), parsed);

        string originalText = Format(original);
        string parsedText = Format(parsed);

        File.WriteAllText(Path.Combine(resultsPath, "original_indented.txt"), originalText);
        File.WriteAllText(Path.Combine(resultsPath, "parsed_indented.txt"), parsedText);

        using StreamWriter writer = new StreamWriter(File.Open(Path.Combine(resultsPath, "stats.txt"), FileMode.Create));

        var saveStateRegex = SaveStateSectionRegex();

        IEnumerable<Match> originalMatches = saveStateRegex.Matches(originalText);
        
        foreach (var match in originalMatches)
        {
            string slugcat = match.Groups[1].Value;

            Console.WriteLine($"Writing comparisons for save state {slugcat}.");

            CompareSections(originalText, parsedText, match.Value, "<progDivA>", $"{slugcat} save state", writer);
        }
    }

    private static void CompareSections(string original, string parsed, string start, string end, string comparisonName, StreamWriter writer)
    {
        int originalStart = original.IndexOf(start);
        int parsedStart = parsed.IndexOf(start);

        if (originalStart == -1 || parsedStart == -1)
        {
            Console.WriteLine($"Failed to do comparison for {comparisonName}.");
            return;
        }

        int originalEnd = original.IndexOf(end, originalStart);
        int parsedEnd = parsed.IndexOf(end, parsedStart);

        if (originalEnd == -1 || parsedEnd == -1)
        {
            Console.WriteLine($"Failed to do comparison for {comparisonName}.");
            return;
        }

        int maxLength = Math.Max(original.Length, parsed.Length);

        int originalLine = 1;
        int originalCol = 1;

        int parsedLine = 1;
        int parsedCol = 1;

        bool mismatchFound = false;

        int i = 0, j = 0;

        while (i < originalStart)
        {
            if (original[i] == '\n')
            {
                originalLine++;
                originalCol = 1;
            }
            else
            {
                originalCol++;
            }

            i++;
        }

        while (j < parsedStart)
        {
            if (parsed[j] == '\n')
            {
                parsedLine++;
                parsedCol = 1;
            }
            else
            {
                parsedCol++;
            }

            j++;
        }

        for (i = originalStart, j = parsedStart; i < maxLength && j < maxLength; i++, j++)
        {
            if (i == originalEnd || j == parsedEnd)
            {
                break;
            }

            if (original[i] != parsed[j])
            {
                mismatchFound = true;
                break;
            }

            if (original[i] == '\n')
            {
                originalLine++;
                originalCol = 1;
            }
            else
            {
                originalCol++;
            }

            if (parsed[j] == '\n')
            {
                parsedLine++;
                parsedCol = 1;
            }
            else
            {
                parsedCol++;
            }
        }

        if (!(i == originalEnd && j == parsedEnd))
            mismatchFound = true;

        if (!mismatchFound)
        {
            writer.WriteLine($"{comparisonName} has no differences.");
        }
        else
        {
            var originalPct = (i - originalStart) * 100f / (originalEnd - originalStart);
            var parsedPct = (j - parsedStart) * 100f / (parsedEnd - parsedStart);
            writer.WriteLine($"{comparisonName} differences: {originalPct:F2}% (Line {originalLine}, Col {originalCol}) | {parsedPct:F2}% (Line {parsedLine}, Col {parsedCol})");
        }
    }

    private static string Format(string original)
    {
        StringBuilder result = new();

        for (int i = 0; i < original.Length; i++)
        {
            if (original[i] == '<' && i > 0 && original[i - 1] != '>')
                result.AppendLine();

            result.Append(original[i]);

            if (original[i] == '>')
                result.AppendLine();
        }

        return result.ToString();
    }
}
