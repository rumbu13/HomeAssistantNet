using HomeAssistantNet.Helpers;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaApiCalendarEvent
{
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }

    [JsonConverter(typeof(JsonDateOrTimeConverter))]
    public DateTime? Start { get; set; }

    [JsonConverter(typeof(JsonDateOrTimeConverter))]
    public DateTime? End { get; set; }
}
