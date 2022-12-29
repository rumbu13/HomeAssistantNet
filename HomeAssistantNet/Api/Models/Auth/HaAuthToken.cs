namespace HomeAssistantNet.Api;

public sealed record HaAuthToken
{
    public string? Id { get; init; }
    public string? ClientId { get; init; }
    public string? ClientName { get; init; }
    public string? ClientIcon { get; init; }
    public DateTime? CreatedAt { get; init; }
    public bool? IsCurrent { get; init; }
    public DateTime? LastUsedAt { get; init; }
    public string? LastUsedIp { get; init; }
    public string? AuthProviderType { get; init; }
}
