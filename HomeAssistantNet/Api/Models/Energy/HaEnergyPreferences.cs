namespace HomeAssistantNet.Api;

public sealed record HaEnergyPreferences
{
    public HaEnergySource[]? EnergySources { get; init; }
    public HaDeviceConsumption[]? DeviceConsumption { get; init; }
}
