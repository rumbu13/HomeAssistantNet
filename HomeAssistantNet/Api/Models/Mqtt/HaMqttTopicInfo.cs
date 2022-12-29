namespace HomeAssistantNet.Api;

public sealed record HaMqttTopicInfo
{
    public string? Topic { get; init; }
    public HaMqttMessage[]? Messages { get; init; }
}
