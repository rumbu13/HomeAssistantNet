namespace HomeAssistantNet.Api;

public sealed record HaEcovacsErrorEvent : HaStandardEvent
{
    public HaEcovacsErrorData? Data { get; init; }
}
