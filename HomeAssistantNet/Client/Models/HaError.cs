namespace HomeAssistantNet.Client;

public sealed record HaError
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}