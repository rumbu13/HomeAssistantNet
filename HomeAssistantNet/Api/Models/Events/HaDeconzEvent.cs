namespace HomeAssistantNet.Api;

public sealed record HaDeconzEvent : HaStandardEvent
{
    public HaDeconzData? Data { get; init; }
}
