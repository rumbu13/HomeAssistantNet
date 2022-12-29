namespace HomeAssistantNet.Api;

public sealed record HaAmcrestEvent : HaStandardEvent
{
    public HaAmcrestData? Data { get; init; }
}
