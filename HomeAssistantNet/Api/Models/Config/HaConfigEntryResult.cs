namespace HomeAssistantNet.Api;

public sealed record HaConfigEntryResult
{
    public HaConfigEntry? ConfigEntry { get; init; }
    public bool RequireRestart { get; init; }
}
