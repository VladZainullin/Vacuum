namespace Vacuum.Encoding;

public class Statistic<T> where T : unmanaged
{
    public SortedDictionary<T, Node> Dictionary { get; set; }

    public Statistic(SortedDictionary<T, Node> dictionary)
    {
        Dictionary = dictionary;
    }

    public int Count => Dictionary.Select(e => e.Value.Count).Sum();
}