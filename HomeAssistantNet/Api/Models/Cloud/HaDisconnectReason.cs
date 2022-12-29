namespace HomeAssistantNet.Api;

public sealed record HaDisconnectReason
{
    public bool? Clean { get; init; }
    public string? Reason { get; init; }
}
