namespace HomeAssistantNet.Api;

public sealed record HaDoorbirdResetEvent : HaStandardEvent
{
    public HaDoorbirdResetData? Data { get; init; }
}
