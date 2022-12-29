namespace HomeAssistantNet.Api;

public sealed record HaApplicationCredential
{
    public string? Id { get; init; }
    public string? Domain { get; init; }
    public string? ClientId { get; init; }
    public string? ClientSecret { get; init; }
    public string? AuthDomain { get; init; }
    public string? Name { get; init; }
}
