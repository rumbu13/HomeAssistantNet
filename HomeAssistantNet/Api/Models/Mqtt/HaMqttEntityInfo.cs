namespace HomeAssistantNet.Api;

public sealed record HaMqttEntityInfo
{
    public string? EntityId { get; init; }
    public HaMqttDiscoveryInfo? DiscoveryData { get; init; }
    public HaMqttTopicInfo[]? Subscriptions { get; init; }
    public HaMqttTopicInfo[]? Transmitted { get; init; }
}
