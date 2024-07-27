using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveEditor.Save;

public class Ghosts : IParsable<Ghosts>
{
    public Dictionary<string, int> GhostStates { get; } = [];

    public List<string> UnrecognizedStates { get; } = [];

    public static Ghosts Parse(string s, IFormatProvider? provider)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var ghost = new Ghosts();

        foreach (var ghostData in s.Split(",", StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = ghostData.Split(":", 2);

            if (parts.Length != 2)
            {
                ghost.UnrecognizedStates.Add(ghostData);
            }
            else
            {
                try
                {
                    ghost.GhostStates[parts[0]] = int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                catch (ArgumentException)
                {
                    ghost.UnrecognizedStates.Add(ghostData);
                }
            }
        }

        return ghost;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Ghosts result)
    {
        throw new NotImplementedException();
    }
}
