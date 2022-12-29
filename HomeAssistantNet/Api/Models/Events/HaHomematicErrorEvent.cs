namespace HomeAssistantNet.Api;

public sealed record HaHomematicErrorEvent : HaStandardEvent
{
    public HaHomematicErrorData? Data { get; init; }
}
