namespace HomeAssistantNet.Api;

public sealed record HaFlowToSource
{
    public string? StatEnergTo { get; init; }
    public string? StatCompensation { get; init; }
    public string? EntityEnergyTo { get; init; }
    public string? EntityEnergyPrice { get; init; }
    public double? NumberEnergyPrice { get; init; }
}
