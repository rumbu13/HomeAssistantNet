namespace HomeAssistantNet.Api;

public sealed record HaLogEntryEvent : HaStandardEvent
{
    public HaLogEntryData? Data { get; init; }
}
