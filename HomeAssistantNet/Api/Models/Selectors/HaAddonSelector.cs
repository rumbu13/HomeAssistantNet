namespace HomeAssistantNet.Api;

public sealed record HaAddonSelector
{
    public string? Name { get; init; }
    public string? Slug { get; init; }
}