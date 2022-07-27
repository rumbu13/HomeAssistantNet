﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api;

public record HaInputBoolean
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public bool? Initial { get; init; }
    public string? Name { get; init; }
}
