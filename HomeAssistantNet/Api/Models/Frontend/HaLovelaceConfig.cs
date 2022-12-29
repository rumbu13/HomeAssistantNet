namespace HomeAssistantNet.Api;

public sealed record HaLovelaceConfig
{
    public string? Title { get; init; }
    public HaLovelaceStrategy? Strategy { get; init; }
    public HaLovelaceView[]? Views { get; init; }
    public string? Background { get; init; }
}