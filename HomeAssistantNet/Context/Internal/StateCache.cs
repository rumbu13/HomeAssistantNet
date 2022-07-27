using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantNet.Context.Internal;

public class EntityStateCache: CacheBase<HaEntityState>
{
    ConcurrentDictionary<string, HaEntityState> states = new();

    public EntityStateCache(IHaWsClient haWsClient)
        : base(haWsClient)
    {        
    }

    protected override void HaWsClient_EventReceived(object? sender, HaWsEventEventArgs e)
    {
        if (e.Event.EventType == "state_changed" && e.Event.Data is not null)
        {
            var data = e.Event.Data.Value.Deserialize<HaStateChangeData>(HaOptions.DefaultJsonSerializerOptions);
            if (data?.EntityId is not null && data?.NewState is not null)
                states[data.EntityId] = data.NewState;
        }
        else if (e.Event.EventType == "homeassistant_started")
            _ = RefreshAsync();
    }


    public override IEnumerable<HaEntityState> GetItems()
    {
        return states.Values;
    }

    public override HaEntityState? GetItem(string key)
    {
        if (states.TryGetValue(key, out var item))
            return item;
        return null;
    }

    protected override async Task<bool> RefreshItems()
    {
        var newStates = await haWsClient.GetStatesAsync(stopCancellation.Token);

        if (newStates != null)
        {
            states = new ConcurrentDictionary<string, HaEntityState>(newStates.Select(s => new KeyValuePair<string, HaEntityState>(s.EntityId!, s)));
            return true;
        }
        return false;
    }
}
