﻿using HomeAssistantNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Context.Internal;

public class Cache<T>: CacheBase<T> where T : class
{
    Dictionary<string, T> items = new();
    readonly Func<CancellationToken, Task<IReadOnlyList<T>?>> itemGetter;
    readonly Func<T, string> keyGetter;
    readonly Func<HaWsEventEventArgs, bool> trigger;


    public Cache(IHaWsClient haWsClient, 
        Func<CancellationToken, Task<IReadOnlyList<T>?>> itemGetter, 
        Func<T, string> keyGetter,
        Func<HaWsEventEventArgs, bool> trigger)
        : base(haWsClient)
    {
        this.itemGetter = itemGetter;
        this.keyGetter = keyGetter;
        this.trigger = trigger;

    }

    protected override void HaWsClient_EventReceived(object? sender, HaWsEventEventArgs e)
    {
        if (trigger(e))
            _ = RefreshAsync();
    }

    public override IEnumerable<T> GetItems()
    {
        return items.Values;
    }

    public override T? GetItem(string key)
    {
        if (items.TryGetValue(key, out var item))
            return item;
        return null;
    }

  

    protected override async Task<bool> RefreshItems()
    {
        var newItems = await itemGetter(stopCancellation.Token);
        if (newItems is not null)
        {
            items = newItems.ToDictionary(i => keyGetter(i));
            return true;
        }
        return false;
    }
}