namespace Vacuum;

public record Statistic(
    decimal Count,
    IDictionary<byte, decimal> Dictionary);
