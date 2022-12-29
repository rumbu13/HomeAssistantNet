namespace HomeAssistantNet.Api;

public sealed record HaDeconzAlarmEvent : HaStandardEvent
{
    public HaDeconzAlarmData? Data { get; init; }
}
