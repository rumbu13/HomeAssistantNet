namespace HomeAssistantNet.Api;

public sealed record HaDiagnosticInfo
{
    public string? Domain { get; init; }
    public HaDiagnosticHandler? Handlers { get; init; }
}
