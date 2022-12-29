namespace HomeAssistantNet.Api;

public sealed record HaLogbookEntryEvent : HaEvent
{
    public HaLogbookEntryData? Data { get; init; }
}
