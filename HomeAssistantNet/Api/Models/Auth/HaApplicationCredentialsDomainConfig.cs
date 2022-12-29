using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaApplicationCredentialsDomainConfig
{
    public IDictionary<string, string>? DescriptionPlaceHolders { get; init; }
}