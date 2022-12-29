namespace HomeAssistantNet.Api;

public sealed record HaArcamTurnOnEvent : HaStandardEvent
{
    public HaArcamTurnOnData? Data { get; init; }
}
