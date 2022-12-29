namespace HomeAssistantNet.Api;

public sealed record HaDynalitePacketData
{
    public string? Host { get; init; }
    public int[]? Packet { get; init; }
}
