namespace HomeAssistantNet.Api;

public sealed record HaDynalitePresetData
{
    public string? Host { get; init; }
    public string? Area { get; init; }
    public string? Preset { get; init; }
}
