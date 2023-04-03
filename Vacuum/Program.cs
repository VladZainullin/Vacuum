using Vacuum.Decoding;
using Vacuum.Encoding;

namespace Vacuum;

internal static class Program
{
    private const string Text = "aaaabbbbbbcddeee";

    public static void Main()
    {
        var encoder = new Encoder<char>(Text);
        var coding = encoder.Coding();

        var decoder = new Decoder<char>(coding);

        var result = decoder.Decode();
        foreach (var e in result)
        {
            Console.Write(e);
        }
    }
}