namespace HomeAssistantNet.Api;

public sealed record HaAreaSelector
{
    public HaEntitySelector? Entity { get; init; }
    public HaDeviceSelector? Device { get; init; }
    public bool? Multiple { get; init; }
}