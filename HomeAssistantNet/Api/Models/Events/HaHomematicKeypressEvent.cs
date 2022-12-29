namespace HomeAssistantNet.Api;

public sealed record HaHomematicKeypressEvent : HaStandardEvent
{
    public HaHomematicKeypressData? Data { get; init; }
}
