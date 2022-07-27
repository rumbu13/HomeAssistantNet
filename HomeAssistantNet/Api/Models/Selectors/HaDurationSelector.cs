using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public sealed record HaDurationSelector
{
    public bool? EnableDay { get; init; }
}
