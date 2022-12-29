namespace HomeAssistantNet.Api;

public sealed record HaScriptStartedData
{
    public string? Name { get; init; }
    public string? EntityId { get; init; }
}
