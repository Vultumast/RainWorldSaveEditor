﻿using RainWorldSaveAPI;
using System.Diagnostics;
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

    const string LoadPathExpedition = "saves_exp";
    const string WritePathExpedition = "results_exp";

    static Stopwatch Stopwatch { get; } = new();

    static void Main(string[] args)
    {
        Directory.CreateDirectory(LoadPath);
        Directory.CreateDirectory(WritePath);
        Directory.CreateDirectory(LoadPathExpedition);
        Directory.CreateDirectory(WritePathExpedition);

        var saveFiles = Directory.GetFiles(LoadPath);

        if (saveFiles.Length == 0)
        {
            Console.WriteLine($"No saves found, put the saves you want to test under \"{LoadPath}\".");
        }
        else
        {
            Console.WriteLine($"Found {saveFiles.Length} save files under \"{LoadPath}\".");

            Stopwatch.Start();
            foreach (var file in saveFiles.Select(Path.GetFileName).Where(x => x != null).Cast<string>())
            {
                RunRWSaveSerializationTest(file);
            }
            Stopwatch.Stop();
        }

        var expeditionFiles = Directory.GetFiles(LoadPathExpedition);

        if (expeditionFiles.Length == 0)
        {
            Console.WriteLine($"No expedition files found, put the saves you want to test under \"{LoadPathExpedition}\".");
        }
        else
        {
            Console.WriteLine($"Found {expeditionFiles.Length} expedition files under \"{LoadPathExpedition}\".");

            Stopwatch.Start();
            foreach (var file in expeditionFiles.Select(Path.GetFileName).Where(x => x != null).Cast<string>())
            {
                RunRWExpeditionSerializationTest(file);
            }
            Stopwatch.Stop();

            Console.WriteLine("Open output folder? Y/n: ");

            if (char.ToLower(Console.ReadKey(true).KeyChar) == 'y')
            {
                Console.WriteLine("Opening folder...");
                Process.Start("explorer.exe", Directory.GetCurrentDirectory());
            }
        }
    }

    private static void RunRWExpeditionSerializationTest(string file)
    {
        Console.WriteLine($"Checking expedition file {file}...");

        try
        {
            var inputPath = Path.Combine(LoadPathExpedition, file);
            var resultsPath = Path.Combine(WritePathExpedition, file);

            Directory.CreateDirectory(resultsPath);

            using var apiLoggerStream = new StreamWriter(File.Open(Path.Combine(resultsPath, "apilog.txt"), FileMode.Create));
            Logger.LogStreamWriter = apiLoggerStream;
            Logger.LogToConsole = false;

            GetExpeditionOriginalAndParsed(inputPath, out var original, out var parsed);
            WriteExpeditionComparisons(resultsPath, original, parsed);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an exception while checking file {file}!");
            Console.WriteLine($"{e}");
        }
    }

    private static void RunRWSaveSerializationTest(string file)
    {
        Console.WriteLine($"Checking save file {file}...");

        try
        {
            var inputPath = Path.Combine(LoadPath, file);
            var resultsPath = Path.Combine(WritePath, file);

            Directory.CreateDirectory(resultsPath);

            using var apiLoggerStream = new StreamWriter(File.Open(Path.Combine(resultsPath, "apilog.txt"), FileMode.Create));
            Logger.LogStreamWriter = apiLoggerStream;
            Logger.LogToConsole = false;

            GetSaveOriginalAndParsed(inputPath, out var original, out var parsed);
            WriteSaveComparisons(resultsPath, original, parsed);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an exception while checking file {file}!");
            Console.WriteLine($"{e}");
        }
    }

    private static void GetExpeditionOriginalAndParsed(string path, out string original, out string parsed)
    {
        var startTime = Stopwatch.Elapsed;

        using var fs = File.OpenRead(path);

        var table = HashtableSerializer.Read(fs);
        var save = new ExpeditionCoreSave();

        if (table["core"] is not string saveData)
            throw new InvalidOperationException("Expected expedition hashtable to have a \"core\" field.");

        save.Read(saveData);

        var readTime = Stopwatch.Elapsed;

        var serializedData = save.Write();

        var writeTime = Stopwatch.Elapsed;

        original = saveData;
        parsed = serializedData;

        Console.WriteLine($"Time taken - read: {(readTime - startTime).Milliseconds}ms, write: {(writeTime - readTime).Milliseconds}ms.");
    }

    private static void WriteExpeditionComparisons(string resultsPath, string original, string parsed)
    {
        File.WriteAllText(Path.Combine(resultsPath, "original.txt"), original);
        File.WriteAllText(Path.Combine(resultsPath, "parsed.txt"), parsed);

        string originalText = Format(original);
        string parsedText = Format(parsed);

        File.WriteAllText(Path.Combine(resultsPath, "original_indented.txt"), originalText);
        File.WriteAllText(Path.Combine(resultsPath, "parsed_indented.txt"), parsedText);

        using StreamWriter writer = new StreamWriter(File.Open(Path.Combine(resultsPath, "stats.txt"), FileMode.Create));

        CompareSections(originalText, parsedText, originalText[0..10], originalText[^10..^0], $"Expedition save data", writer);
    }

    private static void GetSaveOriginalAndParsed(string path, out string original, out string parsed)
    {
        var startTime = Stopwatch.Elapsed;

        using var fs = File.OpenRead(path);

        var table = HashtableSerializer.Read(fs);
        var save = new RainWorldSave();

        if (table["save"] is not string saveData)
            throw new InvalidOperationException("Expected save hashtable to have a \"save\" field.");

        save.Read(saveData);

        var readTime = Stopwatch.Elapsed;

        var serializedData = save.Write();

        var writeTime = Stopwatch.Elapsed;

        original = saveData;
        parsed = serializedData;

        Console.WriteLine($"Time taken - read: {(readTime - startTime).Milliseconds}ms, write: {(writeTime - readTime).Milliseconds}ms.");
    }

    [GeneratedRegex("SAV STATE NUMBER(?:\r|\n|\r\n)<svB>(?:\r|\n|\r\n)([^<\r\n]*)(?:\r|\n|\r\n)<svA>")]
    private static partial Regex SaveStateSectionRegex();

    private static void WriteSaveComparisons(string resultsPath, string original, string parsed)
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
