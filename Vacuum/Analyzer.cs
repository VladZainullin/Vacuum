namespace Vacuum;

public class Analyzer<T> where T : unmanaged
{
    private readonly IEnumerable<T> _bytes;
    
    private readonly Dictionary<T, int> _statistic = new();

    public Analyzer(IEnumerable<T> bytes)
    {
        _bytes = bytes;
    }

    public void Analyze()
    {
        foreach (var key in _bytes)
        {
            if (_statistic.ContainsKey(key))
            {
                _statistic[key]++;
                continue;
            }

            _statistic.Add(key, 1);
        }
    }

    public void Coding()
    {
        var tree = new List<Node>();

        foreach (var pair in _statistic)
        {
            var node = new Node
            {
                Key = pair.Key.ToString(),
                Count = pair.Value,
                Code = string.Empty
            };
            
            tree.Add(node);
        }

        for (var i = 0; i < _statistic.Count - 1; i++)
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
            
        SetValues(head);
            
        foreach (var pair in tree)
        {
            Console.WriteLine($"Key: {pair.Key}; Count: {pair.Count}; Code: {pair.Code}");
        }
    }
    
    private static void SetValues(Node head, string value = "")
    {
        head.Code = value;
        
        if (head.Left != null)
        {
            SetValues(head.Left, value + "1");
        }

        if (head.Right != null)
        {
            SetValues(head.Right, value + "0");
        }
    }
}