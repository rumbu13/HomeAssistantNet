namespace HomeAssistantNet.Api;

public sealed record HaTemplateTrigger : HaTrigger
{
    public string? ValueTemplate { get; init; }
}
