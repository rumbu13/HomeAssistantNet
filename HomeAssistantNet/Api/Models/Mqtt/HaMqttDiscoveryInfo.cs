namespace HomeAssistantNet.Api;

public sealed record HaMqttDiscoveryInfo
{
    public string? Topic { get; init; }
    public string? Payload { get; init; }
}
