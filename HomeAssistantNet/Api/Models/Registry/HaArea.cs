namespace HomeAssistantNet.Api;

public sealed record HaArea
{
    public string? AreaId { get; init; }
    public string? Name { get; init; }
    public string? Picture { get; init; }
}
