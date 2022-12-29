using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaParallelAction : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Parallel { get; init; }
}
