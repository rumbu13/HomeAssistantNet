namespace HomeAssistantNet.Api;

public sealed record HaScriptData
{
    public string? Name { get; init; }
    public string? EntityId { get; init; }
}
