namespace HomeAssistantNet.Api;

public sealed record HaRepeatAction : HaRepeatActionBase
{
    public HaRepeatActionBase? Repeat { get; init; }
}
