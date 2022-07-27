namespace HomeAssistantNet.Client;

public interface IHaRestClient : IDisposable
{
    void Start(HaRestClientOptions options);
    void Stop();

    bool IsRunning { get; }

    Task<T?> GetAsync<T>(string apiPath, CancellationToken cancellationToken = default);
    Task<TResult?> PostAsync<T, TResult>(string apiPath, T? value, CancellationToken cancellationToken = default);
    Task<T?> DeleteAsync<T>(string apiPath, CancellationToken cancellationToken = default);
    Task<string?> GetTextAsync(string apiPath, CancellationToken cancellationToken = default);
    Task<Stream?> GetStreamAsync(string apiPath, CancellationToken cancellationToken = default);
    public Task<string?> PostTextAsync<T>(string apiPath, T? value, CancellationToken cancellationToken = default);
}
