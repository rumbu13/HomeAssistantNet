namespace HomeAssistantNet.Api;

public sealed record HaMqttDeviceInfo
{
    public HaMqttEntityInfo[]? Entities { get; init; }
    public HaMqttTriggerInfo[]? Triggers { get; init; }
}
