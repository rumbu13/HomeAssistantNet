namespace HomeAssistantNet.Api;

public sealed record HaLocationSelector
{
    public bool? Radius { get; init; }
    public string? Icon { get; init; }
}