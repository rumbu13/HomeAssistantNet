namespace HomeAssistantNet.Api;

public sealed record HaPlaceholder
{
    public string? Name { get; init; }
    public double Index { get; init; }
    public string[]? Fields { get; set; }
    public string[]? Domains { get; set; }
    public string[]? DeviceClasses { get; set; }
}
