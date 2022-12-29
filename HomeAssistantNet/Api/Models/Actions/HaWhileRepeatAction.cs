namespace HomeAssistantNet.Api;

public sealed record HaWhileRepeatAction : HaRepeatActionBase
{
    public HaCondition[]? While { get; init; }
}
