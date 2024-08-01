using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

public class RegionStateList : IList<RegionState>, IParsable<RegionStateList>
{
    private List<RegionState> _regions = new();

    // vultu: TEMP
    public string data = "???";

    public int Count => ((ICollection<RegionState>)_regions).Count;

    public bool IsReadOnly => ((ICollection<RegionState>)_regions).IsReadOnly;

    public RegionState this[int index] { get => ((IList<RegionState>)_regions)[index]; set => ((IList<RegionState>)_regions)[index] = value; }

    public static RegionStateList Parse(string s, IFormatProvider? provider)
    {
        RegionStateList stateList = new();
        stateList.data = s;

        return stateList;

        var regions = s.Split("<rgB>");

        // This may have invalid / modded / unrecognized regions
        foreach (var region in regions)
        {
            var state = new RegionState();

        }

        return stateList;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RegionStateList result)
    {
        throw new NotImplementedException();
    }

    public string Write()
    {
        throw new NotImplementedException();
    }

    public int IndexOf(RegionState item)
    {
        return ((IList<RegionState>)_regions).IndexOf(item);
    }

    public void Insert(int index, RegionState item)
    {
        ((IList<RegionState>)_regions).Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        ((IList<RegionState>)_regions).RemoveAt(index);
    }

    public void Add(RegionState item)
    {
        ((ICollection<RegionState>)_regions).Add(item);
    }

    public void Clear()
    {
        ((ICollection<RegionState>)_regions).Clear();
    }

    public bool Contains(RegionState item)
    {
        return ((ICollection<RegionState>)_regions).Contains(item);
    }

    public void CopyTo(RegionState[] array, int arrayIndex)
    {
        ((ICollection<RegionState>)_regions).CopyTo(array, arrayIndex);
    }

    public bool Remove(RegionState item)
    {
        return ((ICollection<RegionState>)_regions).Remove(item);
    }

    public IEnumerator<RegionState> GetEnumerator()
    {
        return ((IEnumerable<RegionState>)_regions).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_regions).GetEnumerator();
    }
}
