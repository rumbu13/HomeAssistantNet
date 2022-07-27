namespace HomeAssistantNet.Client;

public enum HaDisconnectReason
{
    AuthenticationFailed,
    UserInitiated,
    Error,
    Timeout,
    ConnectionClosed,
    InvalidData,
    CommunicationError,
}
