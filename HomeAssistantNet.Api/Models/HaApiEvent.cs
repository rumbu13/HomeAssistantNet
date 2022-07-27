namespace HomeAssistantNet.Api;

public record HaApiEvent
{
    public string? Event { get; init; }
    public int? ListenerCount { get; init; }

}