namespace HomeAssistantNet.Api;

public sealed record HaGoogleEntityConfig
{
    public bool? ShouldExpose { get; init; }
    public string? OverrideName { get; init; }
    public string[]? Aliases { get; init; }
    public bool? Disable_2fa { get; init; }
}
