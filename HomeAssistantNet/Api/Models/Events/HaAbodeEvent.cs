namespace HomeAssistantNet.Api;

public sealed record HaAbodeEvent : HaStandardEvent
{
    public HaAbodeData? Data { get; init; }
}
