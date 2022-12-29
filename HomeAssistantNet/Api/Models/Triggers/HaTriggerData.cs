using HomeAssistantNet.Client;
using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public record HaTriggerData
{
    public string? Id { get; init; }
    public string? Idx { get; init; }    
}
