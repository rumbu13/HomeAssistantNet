namespace HomeAssistantNet.Api;

public record HaInputDateTime
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public DateTime? Initial { get; init; }
    public string? Name { get; init; }
    public bool? HasDate { get; init; }
    public bool? HasTime { get; init; }
}
