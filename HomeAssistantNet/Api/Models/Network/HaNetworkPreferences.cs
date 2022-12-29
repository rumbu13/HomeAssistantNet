namespace HomeAssistantNet.Api;

public sealed record HaNetworkPreferences
{
    public HaNetworkAdapter[]? Adapters { get; init; }
    public string[]? ConfiguredAdapters { get; init; }
}
