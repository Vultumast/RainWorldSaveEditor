using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace RainWorldSaveAPI.SaveElements;

public class IntList : IParsable<IntList>, IList<int>
{
    private List<int> _list = new();

    public int this[int index] { get => ((IList<int>)_list)[index]; set => ((IList<int>)_list)[index] = value; }

    public int Count => ((ICollection<int>)_list).Count;

    public bool IsReadOnly => ((ICollection<int>)_list).IsReadOnly;

    public static IntList Parse(string s, IFormatProvider? provider)
    {
        IntList list = new();

        var ints = s.Split('.', StringSplitOptions.RemoveEmptyEntries);

        foreach (var @int in ints)
            list.Add(int.Parse(@int));

        return list;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntList result)
    {
        throw new NotImplementedException();
    }

    public void Add(int item)
    {
        ((ICollection<int>)_list).Add(item);
    }

    public void Clear()
    {
        ((ICollection<int>)_list).Clear();
    }

    public bool Contains(int item)
    {
        return ((ICollection<int>)_list).Contains(item);
    }

    public void CopyTo(int[] array, int arrayIndex)
    {
        ((ICollection<int>)_list).CopyTo(array, arrayIndex);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return ((IEnumerable<int>)_list).GetEnumerator();
    }

    public int IndexOf(int item)
    {
        return ((IList<int>)_list).IndexOf(item);
    }

    public void Insert(int index, int item)
    {
        ((IList<int>)_list).Insert(index, item);
    }

    public bool Remove(int item)
    {
        return ((ICollection<int>)_list).Remove(item);
    }

    public void RemoveAt(int index)
    {
        ((IList<int>)_list).RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_list).GetEnumerator();
    }
}
