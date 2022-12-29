namespace HomeAssistantNet.Api;

public sealed record HaCertificateInformation
{
    public string? CommonName { get; init; }
    public string? ExpireDate { get; init; }
    public string? Fingerprint { get; init; }
}
