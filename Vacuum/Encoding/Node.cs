namespace Vacuum;

public class Node
{
    public string? Key { get; set; }

    public int Count { get; set; }
    
    public string? Code { get; set; }
    
    public Node? Left { get; set; }
    
    public Node? Right { get; set; }
    
    public bool IsUse { get; set; }
}