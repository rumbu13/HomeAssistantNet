using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiStatisticsExtensions
{
    public static Task<IDictionary<string, HaStatisticValue[]>?> GetHistoryStatisticsAsync(this IHaClient client,
        DateTime startTime, HaPeriod period, DateTime? endTime = default, IEnumerable<string>? statisticIds = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, HaStatisticValue[]>>(new
        {
            type = "history/statistics_during_period",
            startTime,
            endTime,
            statisticIds = statisticIds?.ToArray(),
            period
        }, cancellationToken);

    public static Task<HaStatistic[]?> GetHistoryStatisticIdsAsync(this IHaClient client, 
        HaStatisticType? statisticType = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaStatistic[]>(new
        {
            type = "history/list_statistic_ids",
            statisticType
        }, cancellationToken);

    public static Task<IDictionary<string, HaHistoryState[]>?> GetHistoryAsync(this IHaClient client,
        DateTime startTime, DateTime? endTime = default, IEnumerable<string>? entityIds = default,
        bool? includeStartTimeState = default, bool? significantChangesOnly = default, bool? minimalResponse = default,
        bool? noAttributes = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, HaHistoryState[]>>(new
        {            
            type = "history/history_during_period",
            startTime,
            endTime,
            entityIds = entityIds?.ToArray(),
            includeStartTimeState,
            significantChangesOnly,
            minimalResponse,
            noAttributes
        }, cancellationToken);

    public static Task<HaLogbookStreamData[]?> GetLogbookEventsAsync(this IHaClient client,
        DateTime startTime, DateTime? endTime = default, IEnumerable<string>? entityIds = default,
        IEnumerable<string>? deviceIds = default, string? contexId = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaLogbookStreamData[]>(new
        {
            type = "logbook/get_events",
            startTime,
            endTime,
            entityIds = entityIds?.ToArray(),
            deviceIds = deviceIds?.ToArray(),
            contexId
        }, cancellationToken);
}