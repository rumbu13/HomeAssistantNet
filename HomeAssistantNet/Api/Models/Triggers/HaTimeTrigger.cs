namespace HomeAssistantNet.Api;

public sealed record HaTimeTrigger: HaTrigger
{
    public string? At { get; init; }
}
