namespace Vacuum.Decoding;

public class Decoder<T> where T : unmanaged
{
    private readonly Coding<T> _coding;

    public Decoder(Coding<T> coding)
    {
        _coding = coding;
    }

    public IEnumerable<T> Decode()
    {
        for (var index = 0; index < _coding.Data.Length - 1; index++)
        {
            var shift = 0;
            
            var value = _coding.Data[index].ToString();

            while (!_coding.Table.ContainsValue(value))
            {
                ++shift;
                var range = new Range(index, index + shift);
                value = _coding.Data[range];
            }
            
            yield return _coding.Table.Single(e => e.Value == value).Key;
        }
    }
}