namespace HomeAssistantNet.Api;

public sealed record HaPerson
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? UserId { get; init; }
    public string[]? DeviceTrackers { get; init; }
    public string? Picture { get; init; }
}
