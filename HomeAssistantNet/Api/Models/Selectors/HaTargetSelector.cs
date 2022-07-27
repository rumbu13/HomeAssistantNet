namespace HomeAssistantNet.Api;

public sealed record HaTargetSelector
{
    public HaEntitySelector? Entity { get; init; }
    public HaDeviceSelector? Device { get; init; }
}