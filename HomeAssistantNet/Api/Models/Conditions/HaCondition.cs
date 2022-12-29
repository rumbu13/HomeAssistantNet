using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonConditionConverter))]
public abstract record HaCondition
{
    public string? Condition { get; private set; }
    public string? Alias { get; private set; }
    public bool? Enabled { get; private set; }

}
