namespace HomeAssistantNet.Api;

public sealed record HaCloudStatus
{
    public bool? LoggedIn { get; init; }
    public string? Cloud { get; init; }
    public bool? HttpUseSsl { get; init; }
    public HaDisconnectReason? CloudLastDisconnectReason { get; init; }
    public string? Email { get; init; }
    public bool? GoogleRegistered { get; init; }
    public HaEntityFilter? GoogleEntities { get; init; }
    public string[]? GoogleDomains { get; init; }
    public bool? AlexaRegistered { get; init; }
    public HaEntityFilter? AlexaEntities { get; init; }
    public HaCloudPreferences? Prefs { get; init; }
    public string? RemoteDomain { get; init; }
    public bool? RemoteConnected { get; init; }
    public HaCertificateInformation? RemoteCertificate { get; init; }
    public bool? ActiveSubscription { get; init; }
}

