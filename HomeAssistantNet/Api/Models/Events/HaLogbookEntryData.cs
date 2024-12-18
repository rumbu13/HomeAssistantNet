﻿namespace HomeAssistantNet.Api;

public sealed record HaLogbookEntryData
{
    public string? Name { get; init; }
    public string? Message { get; init; }
    public string? Domain { get; init; }
    public string? EntityId { get; init; }
}
