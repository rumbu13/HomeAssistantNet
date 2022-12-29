using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaServiceAction : HaAction
{    
    public string? Service { get; init; }
    public string? ServiceTemplate { get; init; }
    public string? EntityId { get; init; }
    public HaServiceTarget? Target { get; init; }
    public IDictionary<string, JsonElement>? Data { get; init; }
}
