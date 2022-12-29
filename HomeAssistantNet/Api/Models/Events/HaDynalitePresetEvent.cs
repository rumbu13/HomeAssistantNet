namespace HomeAssistantNet.Api;

public sealed record HaDynalitePresetEvent : HaStandardEvent
{
    public HaDynalitePresetData? Data { get; init; }
}
