namespace HomeAssistantNet.Api;

public sealed record HaPlateDetectedEvent : HaStandardEvent
{
    public HaPlateDetectedData? Data { get; init; }
}
