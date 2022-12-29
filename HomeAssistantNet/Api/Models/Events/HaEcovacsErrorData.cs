namespace HomeAssistantNet.Api;

public sealed record HaEcovacsErrorData
{
    public string? EntityId { get; init; }
    public string? Error { get; init; }
}
