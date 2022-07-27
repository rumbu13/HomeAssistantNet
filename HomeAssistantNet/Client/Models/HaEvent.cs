using HomeAssistantNet.Api;
using System.Text.Json;

namespace HomeAssistantNet.Client;

public sealed record HaEvent
{
    public JsonElement? Data { get; init; }
    public string? EventType { get; init; }
    public DateTime? TimeFired { get; init; }
    public string? Origin { get; init; }
    public HaStateContext? Context { get; init; }

    public HaStateChangeData? AsStateChange
        => Data?.Deserialize<HaStateChangeData>(HaOptions.DefaultJsonSerializerOptions);

}