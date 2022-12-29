namespace HomeAssistantNet.Api;

public sealed record HaMqttTriggerInfo
{
    public HaMqttDiscoveryInfo? DiscoveryData { get; init; }
}
