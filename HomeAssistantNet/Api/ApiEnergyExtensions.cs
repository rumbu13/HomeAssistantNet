using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiEnergyExtensions
{
    public static Task<HaEnergyInfo?> GetEnergyInfoAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEnergyInfo>(new
        {
            type = "energy/info"
        }, cancellationToken);

    public static Task<HaEnergyValidation?> ValidateEnergyPreferencesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEnergyValidation>(new
        {
            type = "energy/validate"
        }, cancellationToken);

    public static Task<HaEnergyPreferences?> GetEnergyPreferencesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEnergyPreferences>(new
        {
            type = "energy/get_prefs"
        }, cancellationToken);

    public static Task<HaEnergyPreferences?> UpdateEnergyPreferencesAsync(this IHaClient client, 
        IEnumerable<HaEnergySource>? energySources = default, 
        IEnumerable<HaDeviceConsumption>? deviceConsumption = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEnergyPreferences>(new
        {
            type = "energy/save_prefs",
            energySources = energySources?.ToArray(),
            deviceConsumption = deviceConsumption?.ToArray(),
        }, cancellationToken);

    public static Task<IDictionary<string, double>?> GetFossilEnergyConsumptionAsync(this IHaClient client,
        DateTime startTime, IEnumerable<string> energyStatisticIds, string co2StatisticId, string period,
        DateTime? endTime = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, double>>(new
        {
            type = "energy/fossil_energy_consumption",
            startTime,
            endTime,
            energyStatisticIds = energyStatisticIds.ToArray(),
            co2StatisticId,
            period
        }, cancellationToken);

}