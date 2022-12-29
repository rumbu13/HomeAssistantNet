namespace HomeAssistantNet.Api;

public record HaConfigEntry
{
    public string? EntryId { get; init; }
    public string? Domain { get; init; }
    public string? Title { get; init; }
    public string? Source { get; init; }
    public HaConfigEntryState? State { get; init; }
    public bool? SupportsOptions { get; init; }
    public bool? SupportsRemoveDevice{ get; init; }
    public bool? SupportsUnload { get; init; }
    public bool? PrefDisableNewEntities { get; init; }
    public bool? DisablePolling { get; init; }
    public HaDisabledBy? DisabledBy { get; init; }
    public string? Reason { get; init; }


}
