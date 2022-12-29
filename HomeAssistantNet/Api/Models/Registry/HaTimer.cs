using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public record HaTimer
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public string? Name { get; init; }
    public TimeSpan? Duration { get; init; }
    public bool? Restore { get; init; }
}
