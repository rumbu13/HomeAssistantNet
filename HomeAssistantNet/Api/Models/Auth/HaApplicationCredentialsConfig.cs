namespace HomeAssistantNet.Api;

public sealed record HaApplicationCredentialsConfig
{
    public string[]? Domains { get; init; }
    public IDictionary<string, HaApplicationCredentialsDomainConfig>? Integrations { get; init; }
}
