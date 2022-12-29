namespace HomeAssistantNet.Api;

public sealed record HaNetworkAdapter
{
    public string? Name { get; init; }
    public int? Index { get; init; }
    public bool? Enabled { get; init; }
    public bool? Auto { get; init; }
    public bool? Default { get; init; }
    public HaIPV6Address[]? IPV6 { get; init; }
    public HaIPV4Address[]? IPV4 { get; init; }
}
