namespace HomeAssistantNet.Api;

public sealed record HaThingTalkConversion
{
    public HaAutomationConfig? Config { get; init; }
    public IDictionary<string, HaPlaceholder[]>? Placeholders { get; init; }
}
