namespace HomeAssistantNet.Api;

public sealed record HaEnergyInfo
{
    public IDictionary<string, string>? CostSensors { get; init; }
    public string[]? SolarForecastDomains { get; init; }
}
