using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaFlicClickData
{
    public string? ButtonName { get; init; }
    public string? ButtonAddress { get; init; }

    [JsonConverter(typeof(JsonDurationConverter))]
    public TimeSpan? QueuedTime { get; init; }

    public string? ClickType { get; init; }
}
