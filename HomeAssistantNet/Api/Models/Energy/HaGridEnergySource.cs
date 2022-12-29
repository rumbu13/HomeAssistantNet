namespace HomeAssistantNet.Api;

public sealed record HaGridEnergySource: HaEnergySource
{
    public HaFlowFromSource[]? FlowFrom { get; init; }
    public HaFlowToSource[]? FlowTo { get; init; }
    public double? CostAdjustmentDay { get; init; }
}
