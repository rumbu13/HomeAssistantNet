namespace HomeAssistantNet.Api;

internal sealed record HaState
{
    public string? State { get; init; }
    public object? Attributes { get; init; }
}
