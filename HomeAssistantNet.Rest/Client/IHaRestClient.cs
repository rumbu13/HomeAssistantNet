using HomeAssistantNet.Core;

namespace HomeAssistantNet.Rest;

public interface IHaRestClient
{
    Task StartAsync(HaClientOptions options, CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken token = default);

    Task<TResult?> GetAsync<TResult>(string api, CancellationToken cancellationToken = default);
    Task<TResult?> PostAsync<TResult>(string api, object data, CancellationToken cancellationToken = default);
    Task DeleteAsync(string api, CancellationToken cancellationToken = default);



}
