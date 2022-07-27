namespace HomeAssistantNet.Client;

/// <summary>
/// Disconnect reason when connection is lost or failed
/// </summary>
public enum HaWsDisconnectReason
{
    /// <summary>
    /// Authentication failed
    /// </summary>    
    AuthenticationFailed,

    /// <summary>
    /// User cancelled connection
    /// </summary>
    UserInitiated,

    /// <summary>
    /// Unknown error was received
    /// </summary>
    Error,

    /// <summary>
    /// Timeout was encountered while sending, receiving or connecting
    /// </summary>
    Timeout,

    /// <summary>
    /// Server closed current connection
    /// </summary>
    ConnectionClosed,

    /// <summary>
    /// Invalid data was received
    /// </summary>
    InvalidData,
    CommunicationError,
}
