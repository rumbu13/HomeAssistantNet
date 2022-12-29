namespace HomeAssistantNet.Api;

public sealed record HaGasEnergySource : HaEnergySource
{
    public string? StatEnergyFrom { get; init; }
    public string? StatCost { get; init; }
    public string? EntityEnergyFrom { get; init; }
    public string? EntityEnergyPrice { get; init; }
    public double? NumberEnergyFrom { get; init; }
}
