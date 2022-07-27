namespace HomeAssistantNet.Api;

public sealed record HaIntentData
{
    public string? Name { get; init; }
    public Object? Data { get; init; }
}
