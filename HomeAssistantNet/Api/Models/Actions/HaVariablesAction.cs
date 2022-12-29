using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaVariablesAction : HaAction
{
    public IDictionary<string, JsonElement>[]? Variables { get; init; }
}
