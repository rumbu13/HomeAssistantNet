using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantNet.States;

public class HaStateManager : IHaStateManager
{
    ConcurrentDictionary<string, HaEntityState> storage = new();
    IHaWsClient HaWsClient;

    public HaStateManager(IHaWsClient haWsClient)
    {
        this.HaWsClient = haWsClient;

        haWsClient.EventReceived += HaWsClient_EventReceived;
    }

    private void HaWsClient_EventReceived(object? sender, HaWsEventEventArgs e)
    {
        if (e.Event?.EventType == "state_change")
        {
            
        }
    }

    public event EventHandler<EntityStateChangedEventArgs>? StateChanged;

    public IEnumerable<HaEntityState> GetStates()
    {
        return storage.Values;
    }
}
