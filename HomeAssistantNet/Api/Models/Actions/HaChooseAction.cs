using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaChooseAction : HaAction
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaChooseActionChoice>))]
    public HaChooseActionChoice[]? Choose { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaAction>))]
    public HaAction[]? Default { get; init; }
}
