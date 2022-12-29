namespace HomeAssistantNet.Api;

public sealed record HaMfaModule
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public bool? Enabled { get; init; }
}
