namespace Vacuum;

public class Encoder<T> where T : unmanaged
{
    private readonly Statistic<T> _statistic;

    private readonly List<Node> _nodes = new();

    public Encoder(Statistic<T> statistic)
    {
        _statistic = statistic;
    }

    public void Coding()
    {
        CreateLeafs();
        
        for (var i = 0; i < _statistic.Dictionary.Count - 1; i++)
        {
            var min1 = _nodes
                .Where(p => !p.IsUse)
                .MinBy(p => p.Count);
            min1.IsUse = true;
            
            var min2 = _nodes
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

            _nodes.Add(node);
        }
        
        var head = _nodes[^1];
            
        SetCodes(head);

        var enumerable = _nodes
            .Where(n => _statistic.Dictionary
                .Select(s => s.Key.ToString())
                .Contains(n.Key))
            .OrderByDescending(n => n.Count)
            .ToArray();

        var startSize = _statistic.Count * 8;
        Console.WriteLine($"Start size: {startSize} bites");
        
        var endSize = enumerable.Select(n => n.Code.Length * n.Count).Sum();
        Console.WriteLine($"End size: {endSize} bites");

        Console.WriteLine($"Economy: {startSize - endSize}");

        foreach (var pair in enumerable)
        {
            Console.WriteLine($"Key: {pair.Key}; Count: {pair.Count}; Code: {pair.Code}");
        }
    }
    
    private void CreateLeafs()
    {
        foreach (var pair in _statistic.Dictionary)
        {
            var node = new Node
            {
                Key = pair.Key.ToString(),
                Count = pair.Value,
                Code = string.Empty
            };
            
            _nodes.Add(node);
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