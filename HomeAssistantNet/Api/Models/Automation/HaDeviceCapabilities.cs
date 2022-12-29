namespace HomeAssistantNet.Api;

public sealed record HaDeviceCapabilities
{
    public HaSchema[]? ExtraFields { get; set; }
}
