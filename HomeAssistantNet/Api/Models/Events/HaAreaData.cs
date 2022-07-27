using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaAreaData
{
    public string? Action { get; init; }
    public string? AreaId { get; init; }

   

}
