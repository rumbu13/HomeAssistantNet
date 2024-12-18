﻿namespace HomeAssistantNet.Api;

public sealed record HaAutomationTriggeredData
{
    public string? Name { get; init; }
    public string? EntityId { get; init; }
    public string? Source { get; init; }
}
