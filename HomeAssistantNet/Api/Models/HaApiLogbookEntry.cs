using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public sealed record HaApiLogbookEntry
{
    public string? ContextUserId { get; init; }
    public string? Domain { get; init; }
    public string? EntityId { get; init; }
    public string? Message { get; init; }
    public string? Name { get; init; }
    public string? State { get; init; }
    public string? Icon { get; init; }
    public DateTime? When { get; init; }
}
