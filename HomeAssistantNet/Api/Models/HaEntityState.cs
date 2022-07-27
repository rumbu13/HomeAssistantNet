using HomeAssistantNet.Client;
using HomeAssistantNet.Helpers;
using System.Text.Json;

namespace HomeAssistantNet.Api;

public record HaEntityState
{
    public string? EntityId { get; init; }
    public string? State { get; set; }
    public JsonElement? Attributes { get; set; }
    public DateTime? LastChanged { get; set; }
    public DateTime? LastUpdated { get; set; }
    public HaStateContext? Context { get; init; }
}

public record HaEntityState<T> : HaEntityState where T : class
{
    public new T? Attributes
        => base.Attributes?.Deserialize<T>(HaOptions.DefaultJsonSerializerOptions);
}

