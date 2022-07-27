using HomeAssistantNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Context.Internal;

public abstract class CacheBase<T>: ICache<T> where T : class
{
    protected readonly SemaphoreSlim semaphoreSlim = new(1);
    protected readonly CancellationTokenSource stopCancellation = new();
    protected readonly IHaClient haWsClient;
    protected bool disposed;
    protected readonly TaskCompletionSource<bool> loaded = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public bool IsLoaded => loaded.Task.IsCompleted;

    public CacheBase(IHaClient haWsClient)
    {
        this.haWsClient = haWsClient;

        if (haWsClient.IsConnected)
            _ = RefreshAsync();

        haWsClient.Connected += HaWsClient_Connected;

        haWsClient.EventReceived += HaWsClient_EventReceived;
    }

    protected abstract void HaWsClient_EventReceived(object? sender, HaEventEventArgs e);

    protected virtual void HaWsClient_Connected(object? sender, HaConnectedEventArgs e)
    {
        _ = RefreshAsync();
    }

    public abstract IEnumerable<T> GetItems();

    public abstract T? GetItem(string key);

    protected abstract Task<bool> RefreshItems();

    protected async Task RefreshAsync()
    {
        await semaphoreSlim.WaitAsync(stopCancellation.Token);
        try
        {
            loaded.TrySetResult(await RefreshItems());
        }
        catch
        {
            loaded.TrySetResult(false);
        }
        finally
        {
            
            semaphoreSlim.Release();
        }
    }

    public Task WaitForLoadAsync()
    {
        return loaded.Task;
    }

    public void Dispose()
    {
        if (!disposed)
        {
            disposed = true;            
            haWsClient.Connected -= HaWsClient_Connected;
            haWsClient.EventReceived -= HaWsClient_EventReceived;
            stopCancellation.Cancel();
            stopCancellation.Dispose();
            semaphoreSlim.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
