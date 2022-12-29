using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaEventAction : HaAction
{    
    public string? Event { get; init; }
    public IDictionary<string, JsonElement>? EventData { get; init; }
    public IDictionary<string, JsonElement>? EventDataTemplate { get; init; }
}
