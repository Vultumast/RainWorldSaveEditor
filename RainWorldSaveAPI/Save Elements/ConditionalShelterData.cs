using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI;

[DebuggerDisplay("Shelter = {ShelterName} | Slugcats = {string.Join(\", \", SlugcatFoundList)}")]
public class ConditionalShelterData : IParsable<ConditionalShelterData>
{
    public string ShelterName { get; set; } = "";

    public List<string> SlugcatFoundList { get; set; } = [];

    public static ConditionalShelterData Parse(string s, IFormatProvider? provider)
    {
        var data = new ConditionalShelterData();

        var parts = s.Split(" : ", StringSplitOptions.RemoveEmptyEntries);

        data.ShelterName = parts[0];
        data.SlugcatFoundList.AddRange(parts[1..]);

        return data;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ConditionalShelterData result)
    {
        throw new NotImplementedException();
    }
}
