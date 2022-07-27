namespace HomeAssistantNet.Api;

public sealed record HaServiceData
{
    public string? Domain { get; init; }
    public string? Service { get; init; }
}
