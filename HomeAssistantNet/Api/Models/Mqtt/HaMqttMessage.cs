using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaMqttMessage
{
    public string? Topic { get; init; }
    public string? Payload { get; init; }
    public double? Qos { get; init; }
    public double? Retain { get; init; }

    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime? Time { get; init; }
}
