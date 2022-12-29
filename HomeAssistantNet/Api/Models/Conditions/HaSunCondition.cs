namespace HomeAssistantNet.Api;

public sealed record HaSunCondition : HaCondition
{
    public double? AfterOffset { get; init; }
    public double? BeforeOffset { get; init; }
    public HaSunEvent? After { get; init; }
    public HaSunEvent? Before { get; init; }
}
