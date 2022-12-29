namespace HomeAssistantNet.Api;

public sealed record HaBatteryEnergySource : HaEnergySource
{
    public string? StatEnergyFrom { get; init; }
    public string? StatEnergyTo { get; init; }
}
