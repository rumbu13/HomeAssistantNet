namespace HomeAssistantNet.Api;

public record HaConfig
{
    public IReadOnlyList<string>? Components { get; init; }
    public string? ConfigDir { get; init; }
    public int? Elevation { get; init; }
    public double? Latitude { get; init; }
    public string? LocationName { get; init; }
    public double? Longitude { get; init; }
    public string? TimeZone { get; init; }
    public HaUnitSystem? UnitSystem { get; init; }
    public string? Version { get; init; }
    public IReadOnlyList<string>? WhitelistExternalDirs { get; init; }
    public IReadOnlyList<string>? AllowlistExternalDirs { get; init; }
    public IReadOnlyList<string>? AllowlistExternalUrls { get; init; }
    public string? ConfigSource { get; init; }
    public bool? SafeMode { get; init; }
    public string? ExternalUrl { get; init; }
    public string? InternalUrl { get; init; }
    public string? Currency { get; init; }
    public string? State { get; init; }
}
