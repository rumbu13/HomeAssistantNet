namespace HomeAssistantNet.Api;

public sealed record HaHomematicImpulseEvent : HaStandardEvent
{
    public HaHomematicImpulseData? Data { get; init; }
}
