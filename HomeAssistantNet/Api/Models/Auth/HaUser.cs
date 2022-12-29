namespace HomeAssistantNet.Api;

public sealed record HaUser
{
    public string? Id { get; init; }
    public string? Username { get; init; }
    public string? Name { get; init; }
    public bool? IsOwner { get; init; }
    public bool? IsAdmin { get; init; }
    public bool? IsActive { get; init; }
    public bool? LocalOnly { get; init; }
    public bool? SystemGenerated { get; init; }
    public string[]? GroupIds { get; init; }
    public HaCredential[]? Credentials { get; init; }
    public HaMfaModule[]? MfaModules { get; init; }
}
