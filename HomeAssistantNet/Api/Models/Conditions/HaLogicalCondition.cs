using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLogicalCondition : HaCondition
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaCondition>))]
    public HaCondition[]? Conditions { get; private set; }

}
