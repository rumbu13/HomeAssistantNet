namespace HomeAssistantNet.Api;

public sealed record HaUntilRepeatAction : HaRepeatActionBase
{
    public HaCondition[]? Until { get; init; }
}
