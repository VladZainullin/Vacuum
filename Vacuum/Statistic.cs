namespace Vacuum;

public class Statistic<T> where T : unmanaged
{
    private readonly Dictionary<T, int> _dictionary;

    public Statistic(Dictionary<T, int> dictionary)
    {
        _dictionary = dictionary;
    }

    public int this[T key] => _dictionary[key];

    public int Count => _dictionary.Select(e => e.Value).Sum();
    
    public void Coding()
    {
        var tree = new List<Node>();

        foreach (var pair in _dictionary)
        {
            var node = new Node
            {
                Key = pair.Key.ToString(),
                Count = pair.Value,
                Code = string.Empty
            };
            
            tree.Add(node);
        }

        for (var i = 0; i < _dictionary.Count - 1; i++)
        {
            var min1 = tree
                .Where(p => !p.IsUse)
                .MinBy(p => p.Count);
            min1.IsUse = true;
            
            var min2 = tree
                .Where(p => !p.IsUse)
                .MinBy(p => p.Count);
            min2.IsUse = true;

            var node = new Node
            {
                Key = min1.Key + min2.Key,
                Count = min1.Count + min2.Count,
                Left = min1,
                Right = min2,
            };

            tree.Add(node);
        }
        
        var head = tree[^1];
            
        SetCodes(head);

        var enumerable = tree
            .Where(n => _dictionary
                .Select(s => s.Key.ToString())
                .Contains(n.Key))
            .OrderByDescending(n => n.Count)
            .ToArray();

        var startSize = Count * 8;
        Console.WriteLine($"Start size: {startSize} bites");
        
        var endSize = enumerable.Select(n => n.Code.Length * n.Count).Sum();
        Console.WriteLine($"End size: {endSize} bites");

        Console.WriteLine($"Economy: {startSize - endSize}");

        foreach (var pair in enumerable)
        {
            Console.WriteLine($"Key: {pair.Key}; Count: {pair.Count}; Code: {pair.Code}");
        }
    }
    
    private static void SetCodes(Node head, string value = "")
    {
        head.Code = value;
        
        if (head.Left != null)
        {
            SetCodes(head.Left, value + "1");
        }

        if (head.Right != null)
        {
            SetCodes(head.Right, value + "0");
        }
    }
}