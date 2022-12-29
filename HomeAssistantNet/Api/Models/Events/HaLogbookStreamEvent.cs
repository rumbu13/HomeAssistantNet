using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLogbookStreamEvent : HaEvent
{
    public HaLogbookStreamData[]? Events { get; init; }

    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime? StartTime { get; init; }

    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime? EndTime { get; init; }

    public bool? Partial { get; init; }
}
