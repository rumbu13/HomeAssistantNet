namespace HomeAssistantNet.Api;

public sealed record HaSolarEnergySource: HaEnergySource
{
    public string? StatEnergyFrom { get; init; }
    public string[]? ConfigEntrySolarForecast { get; init; }  
}
