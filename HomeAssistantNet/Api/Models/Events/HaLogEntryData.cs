namespace HomeAssistantNet.Api;

public sealed record HaLogEntryData
{
    public string? Name { get; init; }
    public IReadOnlyList<string>? Message { get; init; }
    public string? Level { get; init; }
    public string? Source { get; init; }
    public DateTime? Timestamp { get; init; }
    public string? Exception { get; init; }
    public int? Count { get; init; }
    public DateTime? FirstOccurred { get; init; }
}
