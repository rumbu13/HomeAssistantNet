namespace HomeAssistantNet.Api;

public sealed record HaEntityFilter
{
    public string[]? IncludeDomains { get; init; }
    public string[]? IncludeEntities { get; init; }
    public string[]? ExcludeDomains { get; init; }
    public string[]? ExcludeEntities { get; init; }
}
