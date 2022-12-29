namespace HomeAssistantNet.Api;

public sealed record HaConversationInfo
{
    public HaConversationOnboardingInfo? Onboarding { get; init; }
    public HaConversationAttributionInfo? Attribution { get; init; }
}
