using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RainWorldSaveAPI.SaveElements;


public enum EchoState
{
    /// <summary>
    /// The echo has not been met yet.
    /// </summary>
    NotMet,
    /// <summary>
    /// The Echo's area has been visited in a previous cycle
    /// </summary>
    Visited,
    /// <summary>
    /// The Echo has been met and increased your karma
    /// </summary>
    Met,
}

public class Echos : IParsable<Echos>
{
    public Dictionary<string, EchoState> EchoStates { get; } = [];

    public List<string> UnrecognizedStates { get; } = [];

    public static Echos Parse(string s, IFormatProvider? provider)
    {
        // TODO: This has a backwards compatible format that needs to be added
        var ghost = new Echos();

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
                    ghost.EchoStates[parts[0]] = (EchoState)int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                catch (ArgumentException)
                {
                    ghost.UnrecognizedStates.Add(ghostData);
                }
            }
        }

        return ghost;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Echos result)
    {
        throw new NotImplementedException();
    }
}
