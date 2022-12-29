namespace HomeAssistantNet.Api;

public sealed record HaMqttEvent : HaEvent
{
    public string? Topic { get; init; }
    public string? Payload { get; init; }
    public int? Qos { get; init; }
    public bool? Retain { get; init; }

}