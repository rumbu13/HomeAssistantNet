using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaPlayMediaAction : HaAction
{    
    public string? Service { get; init; }
    public string? EntityId { get; init; }
    public HaServiceTarget? Target { get; init; }
    public HaPlayMediaData? Data { get; init; }
    public IDictionary<string, JsonElement>? Metadata { get; init; }
}
