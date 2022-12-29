namespace HomeAssistantNet.Api;

public sealed record HaFaceDetectedEvent : HaStandardEvent
{
    public HaFaceDetectedData? Data { get; init; }
}
