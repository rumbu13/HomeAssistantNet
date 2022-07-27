namespace HomeAssistantNet.Api;

public sealed record HaTextSelector
{
    public bool? Multiline { get; init; }
    public string? Suffix { get; init; }
    public string? Type { get; init; }

}