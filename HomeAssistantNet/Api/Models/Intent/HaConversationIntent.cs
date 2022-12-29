using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaConversationIntent
{
    public IDictionary<string, JsonElement>? Speech { get; init; }
    public IDictionary<string, JsonElement>? Reprompt { get; init; }
    public IDictionary<string, IDictionary<string, string>>? Card { get; init; }
}
