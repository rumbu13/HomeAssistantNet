namespace HomeAssistantNet.Api;

public sealed record HaIPV6Address
{
    public string? Address { get; init; }
    public int? Flowinfo { get; init; }
    public int? ScopeId { get; init; }
    public int? NetworkPrefix { get; init; }
}
