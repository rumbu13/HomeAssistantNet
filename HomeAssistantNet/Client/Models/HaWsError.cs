namespace HomeAssistantNet.Client;

/// <summary>
///  JSON format error sent by Home Assistant through WebSocket
/// </summary>
public sealed record HaWsError
{
    public string? Code { get; init; }
    public string? Message { get; init; }
}