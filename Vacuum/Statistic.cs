namespace Vacuum;

public class Statistic<T> where T : unmanaged
{
    public Dictionary<T, int> Dictionary { get; set; }

    public Statistic(Dictionary<T, int> dictionary)
    {
        Dictionary = dictionary;
    }

    public int Count => Dictionary.Select(e => e.Value).Sum();
}