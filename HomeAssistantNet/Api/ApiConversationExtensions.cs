using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiConversationExtensions
{
    public static Task<HaConversationIntent?> ProcessConversationAsync(this IHaClient client, string text, 
        string? conversationId = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaConversationIntent>(new
        {
            type = "conversation/process",
            text,
            conversationId,
        }, cancellationToken);

    public static Task<HaConversationInfo?> GetConversationInfoAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaConversationInfo>(new
        {
            type = "conversation/agent/info"
        }, cancellationToken);

    public static Task<bool?> SetConversationOnboardingAsync(this IHaClient client, bool shown,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<bool?>(new
        {
            type = "conversation/onboarding/set",
            shown
        }, cancellationToken);

}