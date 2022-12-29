using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaLovelaceResource
{
    public string? Id { get; init; }
    public string? Url { get; init; }
    public HaLovelaceResourceType? Type { get; init; }

}