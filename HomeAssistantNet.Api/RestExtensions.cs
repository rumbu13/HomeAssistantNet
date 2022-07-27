using HomeAssistantNet.Client;
using System.Collections.Specialized;
using System.Text;

namespace HomeAssistantNet.Api;

public static class RestExtensions
{


    public static Task<HaApiState?> GetApiState(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaApiState>("/api", cancellationToken);

    public static Task<HaConfig?> GetConfig(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaConfig?>("/api/config", cancellationToken);

    public static Task<IReadOnlyList<HaApiEvent>?> GetEvents(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaApiEvent>>("/api/events", cancellationToken);

    public static Task<IReadOnlyList<HaService>?> GetServices(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaService>>("/api/services", cancellationToken);

    public static Task<IReadOnlyList<HaEntityState>?> GetStatesAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaEntityState>>("/api/states", cancellationToken);

    public static Task<IReadOnlyList<HaApiService>?> GetServicesAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaApiService>>("/api/services", cancellationToken);

    public static Task<HaEntityState?> GetStateAsync(this IHaRestClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaEntityState>($"/api/states/{entityId}", cancellationToken);

    public static Task<IReadOnlyList<HaHistory>?> GetHistory(
        this IHaRestClient client, DateTime? timeStamp, IEnumerable<string>? entityIds = default, DateTime? endTime = default, bool? minimalResponse = default, bool? noAttributes = default, bool? significantChangesOnly = default, CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new StringBuilder("/api/history/period/");
        if (timeStamp is not null)
            sb.Append(timeStamp.Value.ToString("o"));

        NameValueCollection query = new NameValueCollection();
        if (entityIds is not null)
            query.Add("filter_entity_id", String.Join(',', entityIds));
        if (endTime is not null)
            query.Add("end_time", endTime.Value.ToString("o"));
        if (minimalResponse is not null && minimalResponse.Value)
            query.Add("minimal_response", null);
        if (noAttributes is not null && noAttributes.Value)
            query.Add("no_attributes", null);
        if (significantChangesOnly is not null && significantChangesOnly.Value)
            query.Add("significant_changes_only", null);

        string? s = query.ToString();

        if (!string.IsNullOrEmpty(s))
        {
            sb.Append("?");
            sb.Append(s);
        }

        return client.GetAsync<IReadOnlyList<HaHistory>>(sb.ToString());

    }

}