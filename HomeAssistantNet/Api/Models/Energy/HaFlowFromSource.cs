namespace HomeAssistantNet.Api;

public sealed record HaFlowFromSource
{
    public string? StatEnergFrom { get; init; }
    public string? StatCost { get; init; }
    public string? EntityEnergyFrom { get; init; }
    public string? EntityEnergyPrice { get; init; }
    public double? NumberEnergyPrice { get; init; }
}
