using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaEnergyValidation
{
    public HaEnergyValidationIssue[][]? EnergySources { get; init; }
    public HaEnergyValidationIssue[][]? DeviceConsumption { get; init; }
}
