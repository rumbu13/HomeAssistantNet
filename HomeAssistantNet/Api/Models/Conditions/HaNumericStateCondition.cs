namespace HomeAssistantNet.Api;

public sealed record HaNumericStateCondition : HaCondition
{
    public string? EntityId { get; init; }
    public string? Attribute { get; init; }
    public double? Above { get; init; }
    public double? Below { get; init; }
    public string? ValueTemplate { get; init; }

}
