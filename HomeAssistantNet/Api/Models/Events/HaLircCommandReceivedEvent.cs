namespace HomeAssistantNet.Api;

public sealed record HaLircCommandReceivedEvent : HaStandardEvent
{
    public HaLircCommandReceivedData? Data { get; init; }
}
