namespace Vacuum;

public class Analyzer<T> where T : unmanaged
{
    private readonly IEnumerable<T> _bytes;

    public Analyzer(IEnumerable<T> bytes)
    {
        _bytes = bytes;
    }

    public Statistic<T> Analyze()
    {
        var dictionary = new Dictionary<T, int>();
        
        foreach (var key in _bytes)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
                continue;
            }

            dictionary.Add(key, 1);
        }

        return new Statistic<T>(dictionary);
    }
}