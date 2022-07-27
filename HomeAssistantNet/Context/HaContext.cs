using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Context.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Context;

public class HaContext : IHaContext, IDisposable
{
    readonly Cache<HaArea> areaCache;
    readonly Cache<HaDevice> deviceCache;
    readonly Cache<HaEntity> entityCache;
    readonly Cache<HaConfigEntry> configEntryCache;
    readonly Cache<HaService> serviceCache;
    readonly EntityStateCache entityStateCache;
    readonly Cache<HaCounter> counterCache;
    readonly Cache<HaInputBoolean> inputBooleanCache;

    List<ICache> caches = new List<ICache>();

    readonly IHaClient haWsClient;

    bool disposed;


    public HaContext(IHaClient haWsClient)
    {
        this.haWsClient = haWsClient;

        areaCache = new Cache<HaArea>(haWsClient,
            ct => haWsClient.GetAreasAsync(ct),
            a => a.AreaId!,
            args => args.Event.EventType is "area_registry_updated" or "homeassistant_started");
        caches.Add(areaCache);

        deviceCache = new Cache<HaDevice>(haWsClient,
            ct => haWsClient.GetDevicesAsync(ct),
            a => a.Id!,
            args => args.Event.EventType is "device_registry_updated" or "homeassistant_started");
        caches.Add(deviceCache);

        entityCache = new Cache<HaEntity>(haWsClient,
            ct => haWsClient.GetEntitiesExtendedAsync(ct),
            a => a.EntityId!,
            args => args.Event.EventType is "entity_registry_updated" or "homeassistant_started");
        caches.Add(entityCache);

        configEntryCache = new Cache<HaConfigEntry>(haWsClient,
            ct => haWsClient.GetConfigEntriesAsync(null, null, ct),
            a => a.EntryId!,
            args => args.Event.EventType is "core_config_updated" or "config_entry_discovered" or "homeassistant_started");
        caches.Add(configEntryCache);

        serviceCache = new Cache<HaService>(haWsClient,
            ct => haWsClient.GetServicesAsync(ct),
            a => $"{a.Domain}.{a.ServiceId}",
            args => args.Event.EventType is "service_removed" or "service_registered" or "homeassistant_started");
        caches.Add(serviceCache);

        entityStateCache = new EntityStateCache(haWsClient);

        counterCache = new Cache<HaCounter>(haWsClient,
           ct => haWsClient.GetCountersAsync(ct),
           a => a.Id!,
           args => args.Event.EventType is "entity_registry_updated" or "homeassistant_started");
        caches.Add(entityCache);

        inputBooleanCache = new Cache<HaInputBoolean>(haWsClient,
           ct => haWsClient.GetInputBooleansAsync(ct),
           a => a.Id!,
           args => args.Event.EventType is "entity_registry_updated" or "homeassistant_started");
        caches.Add(inputBooleanCache);



    }

    public Task WaitForLoadAsync()
    {
        return Task.WhenAll(caches.Select(c => c.WaitForLoadAsync()));
    }

    public void Dispose()
    {
        if (!disposed)
        {
            disposed = true;
            caches.ForEach(c => c.Dispose());
            GC.SuppressFinalize(this);
        }
    }

    public IEnumerable<HaArea> GetAreas()
        => areaCache.GetItems();

    public IEnumerable<HaDevice> GetDevices()
        => deviceCache.GetItems();

    public IEnumerable<HaEntity> GetEntities()
        => entityCache.GetItems();

    public IEnumerable<HaConfigEntry> GetConfigEntries()
        => configEntryCache.GetItems();

    public IEnumerable<HaService> GetServices()
        => serviceCache.GetItems();

    public IEnumerable<HaEntityState> GetEntityStates()
        => entityStateCache.GetItems();

    public IEnumerable<HaCounter> GetCounters()
        => counterCache.GetItems();

    public IEnumerable<HaInputBoolean> GetInputBooleans()
        => inputBooleanCache.GetItems();

    public HaArea? GetArea(string id)
        => areaCache.GetItem(id);

    public HaDevice? GetDevice(string id)
        => deviceCache.GetItem(id);

    public HaEntity? GetEntity(string id)
        => entityCache.GetItem(id);

    public HaConfigEntry? GetConfigEntry(string id)
        => configEntryCache.GetItem(id);

    public HaService? GetService(string id)
        => serviceCache.GetItem(id);

    public HaEntityState? GetEntityState(string id)
        => entityStateCache.GetItem(id);

    public HaCounter? GetCounter(string id)
        => counterCache.GetItem(id);

    public HaInputBoolean? GetInputBoolean(string id)
        => inputBooleanCache.GetItem(id);
}
