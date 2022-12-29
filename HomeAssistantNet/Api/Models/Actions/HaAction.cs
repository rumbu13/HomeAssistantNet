using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonActionConverter))]
public abstract record HaAction
{
    public string? Alias { get; private set; }
    public bool? Enabled { get; private set; }
}
