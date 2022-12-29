using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaForEachRepeatAction : HaRepeatActionBase
{
    [JsonConverter(typeof(JsonOneOrManyAlwaysStringConverter))]
    public string[]? ForEach { get; init; }
}
