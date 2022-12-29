using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaSupervisorEvent : HaEvent
{
    public string? Event { get; init; }
    public HaSupervisorEventObject? UpdateKey { get; init; }
    public JsonElement? Data { get; init; }

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? OtherData;


}