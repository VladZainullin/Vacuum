namespace Vacuum.Encoding;

public class Node
{
    public Node(
        string key,
        int count = 1,
        Node? left = default,
        Node? right = default)
    {
        Key = key;
        Count = count;
        Left = left;
        Right = right;
    }
    
    public string? Key { get; set; }

    public int Count { get; set; }

    public string Code { get; set; } = string.Empty;
    
    public Node? Left { get; set; }
    
    public Node? Right { get; set; }
    
    public bool IsUse { get; set; }
}