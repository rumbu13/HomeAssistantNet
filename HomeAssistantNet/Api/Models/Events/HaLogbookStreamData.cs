using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLogbookStreamData
{
    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime? When { get; init; }

    public string? Name { get; init; }
    public string? Message { get; init; }
    public string? EntityId { get; init; }
    public string? Icon { get; init; }
    public string? Source { get; init; }
    public string? Domain { get; init; }
    public string? State { get; init; }
    public string? ContextId { get; init; }
    public string? ContextEventType { get; init; }
    public string? ContextDomain { get; init; }
    public string? ContextService { get; init; }
    public string? ContextEntityId { get; init; }
    public string? ContextEntityIdName { get; init; }
    public string? ContextName { get; init; }
    public string? ContextState { get; init; }
    public string? ContextSource { get; init; }
    public string? ContextMessage { get; init; }
}
