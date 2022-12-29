namespace HomeAssistantNet.Api;

public sealed record HaCountRepeatAction : HaRepeatActionBase
{
    public double? Count { get; init; }
}
