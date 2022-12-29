namespace HomeAssistantNet.Api;

public sealed record HaEnoceanButtonPressedEvent : HaStandardEvent
{
    public HaEnoceanButtonPressedData? Data { get; init; }
}
