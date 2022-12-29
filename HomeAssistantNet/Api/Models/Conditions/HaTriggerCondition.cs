namespace HomeAssistantNet.Api;

public sealed record HaTriggerCondition : HaCondition
{
    public string? Id { get; init; }
}
