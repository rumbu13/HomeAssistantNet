using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaConversationOnboardingInfo
{
    public string? Text { get; init; }
    public string? Url { get; init; }
}
