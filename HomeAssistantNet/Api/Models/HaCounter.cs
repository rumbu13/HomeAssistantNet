using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public record HaCounter
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public int? Initial { get; init; }
    public string? Name { get; init; }
    public int? Maximum { get; init; }
    public int? Minimum { get; init; }
    public bool? Restore { get; init; }
    public int? Step { get; init; }
}
