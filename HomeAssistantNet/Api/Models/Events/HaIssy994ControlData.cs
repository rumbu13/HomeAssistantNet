namespace HomeAssistantNet.Api;

public sealed record HaIssy994ControlData
{
    public string? EntityId { get; init; }
    public string? Control { get; init; }
    public int? Value { get; init; }
    public string? Formatted { get; init; }
    public string? Uom { get; init; }
    public string? Precision { get; init; }
}
