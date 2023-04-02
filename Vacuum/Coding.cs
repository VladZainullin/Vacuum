namespace Vacuum;

public class Coding<T> where T : unmanaged
{
    public Coding(Dictionary<T, string> table, string data)
    {
        Table = table;
        Data = data;
    }
    
    public Dictionary<T, string> Table { get; }
    public string Data { get; }
}