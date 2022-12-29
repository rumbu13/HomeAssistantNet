using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaGeolocationTrigger : HaTrigger
{
    public string? Source { get; init; }
    public string? Zone { get; init; }
    public HaGeolocationEvent? Event { get; init; }
}
