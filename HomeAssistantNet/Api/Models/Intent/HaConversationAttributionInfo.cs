using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaConversationAttributionInfo
{
    public string? Name { get; init; }
    public string? Url { get; init; }
}
