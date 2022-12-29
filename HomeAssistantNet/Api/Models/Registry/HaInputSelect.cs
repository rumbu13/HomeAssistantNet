namespace HomeAssistantNet.Api;

public record HaInputSelect
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public string? Name { get; init; }
    public string? Initial { get; init; }
    public string[]? Options { get; init; }
}
