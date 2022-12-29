namespace HomeAssistantNet.Api;

public sealed record HaTemplateCondition : HaCondition
{
    public string? ValueTemplate { get; init; }
}
