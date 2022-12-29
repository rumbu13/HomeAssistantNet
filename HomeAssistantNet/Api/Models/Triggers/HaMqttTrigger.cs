namespace HomeAssistantNet.Api;

public sealed record HaMqttTrigger : HaTrigger
{
    public string? Topic { get; init; }
    public string? Payload { get; init; }

}
