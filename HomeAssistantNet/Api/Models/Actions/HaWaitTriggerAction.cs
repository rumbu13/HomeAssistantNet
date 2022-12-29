using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaWaitTriggerAction : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaTrigger>))]
    public HaTrigger[]? WaitForTrigger { get; init; }
    
    public double? Timeout { get; init; }
    public bool? ContinueOnTimeout { get; init; }
}
