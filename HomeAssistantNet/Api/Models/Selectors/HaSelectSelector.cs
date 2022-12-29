namespace HomeAssistantNet.Api;

public sealed record HaSelectSelector
{
    public HaSelectOption[]? Options { get; init; }
    public bool? Multiple { get; init; }
    public bool? CustomValue { get; init; }
    public HaSelectMode? Mode { get; init; }
}