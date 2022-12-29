using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonEventConverter))]
public abstract record HaEvent
{
    
}