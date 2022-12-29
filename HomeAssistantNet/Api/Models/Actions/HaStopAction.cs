namespace HomeAssistantNet.Api;

public sealed record HaStopAction : HaAction
{
    public string? Stop { get; init; }
    public string? Error { get; init; }
}
