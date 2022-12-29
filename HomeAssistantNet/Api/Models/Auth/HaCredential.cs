namespace HomeAssistantNet.Api;

public sealed record HaCredential
{
    public string? Type { get; init; }
    public string? AuthProviderType { get; init; }
    public string? AuthProviderId { get; init; }
}
