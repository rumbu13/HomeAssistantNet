using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLovelaceView
{
    public double? Index { get; init; }
    public string? Title { get; init; }
    public string? Type { get; init; }
    public HaLovelaceStrategy? Strategy { get; init; }
    public string? Path { get; init; }
    public string? Icon { get; init; }
    public string? Theme { get; init; }
    public bool? Panel { get; init; }
    public string? Background { get; init; }

    public HaLovelaceBadgeConfig[]? Badges { get; init; }

    public HaLovelaceCardConfig[]? Cards { get; init; }

    [JsonConverter(typeof(JsonLovelaceViewConfigConverter))]
    public HaLovelaceViewConfig[]? Visible { get; init; }

}