namespace HomeAssistantNet.Api;

public sealed record HaAlexaSmartHomeEvent : HaStandardEvent
{
    public HaAlexaSmartHomeData? Data { get; init; }
}
