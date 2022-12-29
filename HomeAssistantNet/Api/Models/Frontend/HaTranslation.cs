using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaTranslation
{
    public IDictionary<string, JsonElement>? Resources { get; init; }
}