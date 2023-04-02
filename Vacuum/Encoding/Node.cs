namespace Vacuum.Encoding;

public class Node
{
    private string _code = string.Empty;

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
    
    public string? Key { get; }

    public int Count { get; set; }

    public string Code
    {
        get => _code;
        set
        {
            _code = value;
            
            if (Left != default)
            {
                Left.Code = value + "1";
            }

            if (Right != default)
            {
                Right.Code = value + "0";
            }
        }
    }

    private Node? Left { get; }

    private Node? Right { get; }
    
    public bool IsUse { get; set; }
}