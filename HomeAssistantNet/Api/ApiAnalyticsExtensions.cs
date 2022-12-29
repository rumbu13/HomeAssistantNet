using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiAnalyticsExtensions
{
  
    public static Task<HaAnalytics?> GetAnalyticsAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaAnalytics>(new 
        { 
            type = "analytics" 
        }, cancellationToken);

    public static Task<HaAnalyticsPreferences?> SetAnalyticsAsync(this IHaClient client, bool? @base = default, 
        bool? diagnostics = default, bool? usage = default, bool? statistics = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaAnalyticsPreferences>( new
        {
            type = "analytics/preferences",
            Base = @base,
            diagnostics,
            usage,
            statistics
        }, cancellationToken);



}