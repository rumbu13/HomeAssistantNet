namespace HomeAssistantNet.Api;

public sealed record HaIPV4Address
{
    public string? Address { get; init; }
    public int? NetworkPrefix { get; init; }
}
