namespace HomeAssistantNet.Api;

public sealed record HaGoogleEntity
{
    public string? EntityId { get; init; }
    public string[]? Traits { get; init; }
    public bool? Might_2fa { get; init; }
}
