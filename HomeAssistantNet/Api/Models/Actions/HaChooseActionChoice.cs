using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaChooseActionChoice : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaCondition>))]
    public HaCondition[]? Sequence { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Action { get; init; }
}
