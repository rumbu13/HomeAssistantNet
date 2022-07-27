using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaDeviceUpdate : HaWsCommand
{
    public HaDeviceUpdate(string deviceId, string? areaId, string? nameByUser, string? disabledBy)
        : base("config/device_registry/update")
    {
        DeviceId = deviceId;
        AreaId = areaId;
        NameByUser = nameByUser;
        DisabledBy = disabledBy;
    }
    public string DeviceId { get; init; }
    public string? AreaId { get; init; }
    public string? NameByUser { get; init; }
    public string? DisabledBy { get; init; }
}

internal sealed record HaDeviceRemoveConfigEntry : HaWsCommand
{
    public HaDeviceRemoveConfigEntry(string configEntryId, string deviceId)
        : base("config/device_registry/remove_config_entry")
    {
        ConfigEntryId = configEntryId;
        DeviceId = deviceId;
    }
    
    public string ConfigEntryId { get; init; }
    public string DeviceId { get; init; }
}


