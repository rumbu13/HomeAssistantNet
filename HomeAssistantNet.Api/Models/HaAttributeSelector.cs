namespace HomeAssistantNet.Api;

public sealed record HaAttributeSelector
{
    public string? EntityId { get; init; }
}