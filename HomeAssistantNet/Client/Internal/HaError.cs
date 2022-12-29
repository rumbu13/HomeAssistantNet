namespace HomeAssistantNet.Client;

internal sealed record HaError
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}