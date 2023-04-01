namespace Vacuum;

internal static class Program
{
    private const string Text = "aaaabbbbbbcddeee";

    public static void Main()
    {
        var analyzer = new Analyzer<char>(Text);
        
        analyzer.Analyze();
        analyzer.Coding();
    }
}