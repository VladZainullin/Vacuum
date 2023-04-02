namespace Vacuum;

internal static class Program
{
    private const string Text = "aaaabbbbbbcddeee";

    public static void Main()
    {
        var bytes = File.ReadAllBytes("/Users/vadislavzainullin/MEGAsync/Исмарт/1 семестр/История/Керов История России.doc");
        
        var analyzer = new Analyzer<byte>(bytes);
        
        analyzer.Analyze();
        analyzer.Coding();
    }
}