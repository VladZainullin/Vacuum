namespace Vacuum;

public static class Program
{
    private const string Path = "/Users/vadislavzainullin/MEGAsync/Исмарт/1 семестр/История/Керов История России.doc";
    
    private static async Task Main()
    {
        var exists = File.Exists(Path);
        if (!exists)
        {
            Console.WriteLine("File not found");
            return;
        }
        
        var bytes = await File.ReadAllBytesAsync(Path);

        var statistics = Analyse(bytes);
        
        Print(statistics);
    }

    private static Statistic Analyse(IEnumerable<byte> bytes)
    {
        var dictionary = new SortedDictionary<byte, decimal>();

        var count = 0m;
        
        foreach (var b in bytes)
        {
            count++;
            
            if (dictionary.ContainsKey(b))
            {
                dictionary[b]++;
                continue;
            }
            
            dictionary.Add(b, 1);
        }

        return new Statistic(count, dictionary);
    }

    private static void Print(Statistic statistic)
    {
        Console.WriteLine($"Count: {statistic.Count / 1024 / 1024}mb");
        
        Console.WriteLine($"Key; Count; Percent;");
        
        foreach (var pair in statistic.Dictionary.OrderByDescending(d => d.Value))
        {
            var percent = pair.Value / statistic.Count;
            
            Console.WriteLine($"{pair.Key}; {pair.Value}; {percent}%;");
        }
    }
}