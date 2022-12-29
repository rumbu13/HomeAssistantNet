using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDelayAction : HaAction
{
    [JsonConverter(typeof(JsonDurationConverter))]
    public TimeSpan? Delay { get; init; }
}
