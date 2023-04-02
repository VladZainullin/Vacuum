using System.Text;

namespace Vacuum.Encoding;

public class Encoder<T> where T : unmanaged
{
    private readonly IEnumerable<T> _elements;
    
    private Statistic<T> _statistic = default!;

    public Encoder(IEnumerable<T> elements)
    {
        _elements = elements;
    }

    public Coding<T> Coding()
    {
        Analyze();
        
        CreateHuffmanTree();

        var table = _statistic.Dictionary
            .ToDictionary(
                p => p.Key,
                p => p.Value.Code);
        
        var data = GenerateData();

        Print();

        return new Coding<T>(table, data);
    }

    private void Analyze()
    {
        var dictionary = new SortedDictionary<T, Node>();
        
        foreach (var key in _elements)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Count++;
                continue;
            }

            var node = new Node(key.ToString());
            
            dictionary.Add(key, node);
        }

        _statistic = new Statistic<T>(dictionary);
    }

    private void CreateHuffmanTree()
    {
        var nodes = new List<Node>(_statistic.Dictionary.Values);
        
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

            var node = new Node(
                min1.Key + min2.Key,
                min1.Count + min2.Count,
                min1,
                min2);

            nodes.Add(node);
        }
        
        var root = nodes[^1];
        root.Coding();
    }

    private string GenerateData()
    {
        var builder = new StringBuilder();

        foreach (var element in _elements)
        {
            var node = _statistic.Dictionary[element];

            builder.Append(node.Code);
        }

        return builder.ToString();
    }
    
    private void Print()
    {
        var startSize = _statistic.Count * 8;
        Console.WriteLine($"Start size: {startSize} bites");

        var endSize = _statistic.Dictionary.Values.Select(n => n.Code.Length * n.Count).Sum();
        Console.WriteLine($"End size: {endSize} bites");

        Console.WriteLine($"Economy: {startSize - endSize}");

        foreach (var pair in _statistic.Dictionary.Values)
        {
            Console.WriteLine($"Key: {pair.Key}; Count: {pair.Count}; Code: {pair.Code}");
        }
    }
}