using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public enum HaPeriod
{
    [JsonPropertyName("5minute")]
    FiveMinutes,
    Hour,
    Day,
    Month
}