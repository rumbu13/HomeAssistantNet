using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public sealed record class HaField
{
    public string? Description { get; init; }
    public string? Name { get; init; }
    public JsonElement? Example { get; init; }
    public JsonElement? Default { get; init; }
    public JsonElement? Values { get; init; }
    public bool? Required { get; init; }
    public bool? Advanced { get; init; }
    public HaSelector? Selector { get; init; }
}
