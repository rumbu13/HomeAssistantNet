using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaIfAction : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaCondition>))]
    public HaCondition[]? If { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Then { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Else { get; init; }
}
