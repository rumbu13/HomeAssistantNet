using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public abstract record HaRepeatActionBase : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Sequence { get; init; }
}
