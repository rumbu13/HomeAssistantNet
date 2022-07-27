namespace HomeAssistantNet.Api;

public record HaInputBoolean
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public bool? Initial { get; init; }
    public string? Name { get; init; }
}
