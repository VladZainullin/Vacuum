namespace Vacuum.Encoding;

public class Encoder<T> where T : unmanaged
{
    private readonly IEnumerable<T> _elements;
    
    private Statistic<T> _statistic = default!;

    private readonly List<Node> _nodes = new();

    public Encoder(IEnumerable<T> elements)
    {
        _elements = elements;
    }

    public void Coding()
    {
        Analyze();
        
        CreateLeafs();
        CreateHuffmanTree();

        var startSize = _statistic.Count * 8;
        Console.WriteLine($"Start size: {startSize} bites");
        
        var endSize = _nodes.Select(n => n.Code.Length * n.Count).Sum();
        Console.WriteLine($"End size: {endSize} bites");
        
        Console.WriteLine($"Economy: {startSize - endSize}");
        
        foreach (var pair in _nodes)
        {
            Console.WriteLine($"Key: {pair.Key}; Count: {pair.Count}; Code: {pair.Code}");
        }
    }
    
    private void Analyze()
    {
        var dictionary = new Dictionary<T, int>();
        
        foreach (var key in _elements)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
                continue;
            }

            dictionary.Add(key, 1);
        }

        _statistic = new Statistic<T>(dictionary);
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

    private void CreateHuffmanTree()
    {
        var nodes = new List<Node>(_nodes);
        
        for (var i = 0; i < _statistic.Dictionary.Count - 1; i++)
        {
            var min1 = nodes
                .Where(p => !p.IsUse)
                .MinBy(p => p.Count);
            min1.IsUse = true;

            var min2 = nodes
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

            nodes.Add(node);
        }
        
        var root = nodes[^1];
        SetCodes(root);
    }

    private static void SetCodes(Node root, string value = "")
    {
        root.Code = value;
        
        if (root.Left != null)
        {
            SetCodes(root.Left, value + "1");
        }

        if (root.Right != null)
        {
            SetCodes(root.Right, value + "0");
        }
    }
}