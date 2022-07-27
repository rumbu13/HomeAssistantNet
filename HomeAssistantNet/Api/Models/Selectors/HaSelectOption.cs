namespace HomeAssistantNet.Api;

public sealed record HaSelectOption
{
    public string? Label { get; init; }
    public string? Value { get; init; }
}