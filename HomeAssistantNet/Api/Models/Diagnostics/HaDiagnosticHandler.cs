namespace HomeAssistantNet.Api;

public sealed record HaDiagnosticHandler
{
    public bool? ConfigEntry { get; init; }
    public bool? Device { get; init; }
}
