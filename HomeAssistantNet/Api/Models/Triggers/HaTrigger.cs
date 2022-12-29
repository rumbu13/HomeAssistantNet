using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonConditionConverter))]
public abstract record HaTrigger
{
    public string? Platform { get; private set; }
    public string? Id { get; private set; }
    public IDictionary<string, JsonElement>? Variables { get; private set; }
    public bool? Enabled { get; private set; }

}
