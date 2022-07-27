using HomeAssistantNet.Helpers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace HomeAssistantNet.Client;

public sealed class HaRestClient : IHaRestClient
{
    bool isDisposed;
    bool isRunning;

    CancellationTokenSource? stopCancellation;

    HttpClient? httpClient;

    readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
    };

    void CheckNotDisposed()
    {
        if (isDisposed)
            throw new ObjectDisposedException(nameof(HaRestClient));
    }

    void CheckRunning(bool running)
    {
        if (isRunning != running)
            throw new InvalidOperationException("Home Assistant Client is already " + (running ? "stopped" : "started"));
    }

    public bool IsRunning => isRunning;

    public async Task<T?> DeleteAsync<T>(string apiPath, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.DeleteAsync(new Uri(apiPath), combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync(combined.Token).ConfigureAwait(false);
                if (content.Length == 0)
                    return default;
                return await JsonSerializer.DeserializeAsync<T>(content, jsonOptions, combined.Token).ConfigureAwait(false);
            }
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }

    }

    public async Task<T?> GetAsync<T>(string apiPath, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.GetAsync(new Uri(apiPath), combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync(combined.Token).ConfigureAwait(false);
                if (content.Length == 0)
                    return default;
                return await JsonSerializer.DeserializeAsync<T>(content, jsonOptions, combined.Token).ConfigureAwait(false);

            }
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }

    }

    public async Task<string?> GetTextAsync(string apiPath, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.GetAsync(new Uri(apiPath), combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync(combined.Token).ConfigureAwait(false);
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }

    }

    public async Task<Stream?> GetStreamAsync(string apiPath, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.GetAsync(new Uri(apiPath), combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStreamAsync(combined.Token).ConfigureAwait(false);
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }

    }

    public async Task<TResult?> PostAsync<T, TResult>(string apiPath, T? value, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        var text = value == null ? string.Empty : JsonSerializer.Serialize(value, jsonOptions);
        using var content = new StringContent(text, Encoding.UTF8);

        if (text.Length > 0)
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.PostAsync(new Uri(apiPath), content, combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var receivedContent = await result.Content.ReadAsStreamAsync(combined.Token).ConfigureAwait(false);
                if (receivedContent.Length == 0)
                    return default;
                return await JsonSerializer.DeserializeAsync<TResult>(receivedContent, jsonOptions, combined.Token).ConfigureAwait(false);
            }
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }
    }

    public async Task<string?> PostTextAsync<T>(string apiPath, T? value, CancellationToken cancellationToken = default)
    {
        CheckNotDisposed();
        CheckRunning(true);
        var text = value == null ? string.Empty : JsonSerializer.Serialize(value, jsonOptions);
        using var content = new StringContent(text, Encoding.UTF8);

        if (text.Length > 0)
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

        using var combined = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);

        try
        {
            var result = await httpClient!.PostAsync(new Uri(apiPath), content, combined.Token).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadAsStringAsync(combined.Token).ConfigureAwait(false);
            throw new HttpRequestException(result.ReasonPhrase, null, result.StatusCode);
        }
        catch (OperationCanceledException ex)
        {
            if (ex.CancellationToken == stopCancellation.Token)
                return default;
            else
                throw;
        }
    }

    public void Start(HaRestClientOptions options)
    {
        CheckNotDisposed();
        CheckRunning(false);
        if (options is null)
            throw new ArgumentNullException(nameof(options));
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options!.Token}");
        var schema = options!.UseHttps ? "https" : "http";
        httpClient.BaseAddress = new Uri($"{schema}://{options!.Host}:{options!.Port}");
        stopCancellation = new CancellationTokenSource();
        isRunning = true;
    }

    public void Stop()
    {
        CheckNotDisposed();
        CheckRunning(true);
        stopCancellation?.Cancel();
        httpClient?.CancelPendingRequests();
        httpClient?.Dispose();
        httpClient = null;
        stopCancellation?.Dispose();
        stopCancellation = null;
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            if (stopCancellation != null && !stopCancellation.IsCancellationRequested)
                stopCancellation.Cancel();
            httpClient?.CancelPendingRequests();
            httpClient?.Dispose();
            httpClient = null;
            stopCancellation?.Dispose();
            stopCancellation = null;
        }
    }


}
