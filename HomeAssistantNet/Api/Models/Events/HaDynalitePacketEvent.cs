namespace HomeAssistantNet.Api;

public sealed record HaDynalitePacketEvent : HaStandardEvent
{
    public HaDynalitePacketData? Data { get; init; }
}
