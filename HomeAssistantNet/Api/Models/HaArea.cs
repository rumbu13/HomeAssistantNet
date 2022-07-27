namespace HomeAssistantNet.Api;

public sealed record HaArea
{
    public string? AreaId { get; init; }
    public string? Name { get; set; }
    public string? Picture { get; set; }
}
