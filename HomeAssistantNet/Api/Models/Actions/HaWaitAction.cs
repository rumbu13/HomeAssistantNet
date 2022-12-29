using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaWaitAction : HaAction
{    
    public string? WaitTemplate { get; init; }
    public double? Timeout { get; init; }
    public bool? ContinueOnTimeout { get; init; }
}
