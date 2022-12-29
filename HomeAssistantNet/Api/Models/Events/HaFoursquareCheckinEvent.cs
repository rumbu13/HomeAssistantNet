namespace HomeAssistantNet.Api;

public sealed record HaFoursquareCheckinEvent : HaStandardEvent
{
    public HaFoursquareCheckinData? Data { get; init; }
}
