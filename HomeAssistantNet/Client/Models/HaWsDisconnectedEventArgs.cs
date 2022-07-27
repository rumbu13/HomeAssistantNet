namespace HomeAssistantNet.Client;

/// <summary>
/// Disconnected event
/// </summary>
public sealed class HaWsDisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// Creates a new event
    /// </summary>
    /// <param name="reconnect"></param>
    /// <param name="disconnectReason"></param>
    /// <param name="error"></param>
    public HaWsDisconnectedEventArgs(bool reconnect, HaWsDisconnectReason disconnectReason, Exception? error)
    {
        Reconnect = reconnect;
        DisconnectReason = disconnectReason;
        Error = error;
    }

    /// <summary>
    /// Set to true to allow reconnection
    /// </summary>
    public bool Reconnect { get; private set; }

    /// <summary>
    /// Reason fror disconnection
    /// </summary>
    public HaWsDisconnectReason DisconnectReason { get; private set; }

    /// <summary>
    /// Error leading to disconnection
    /// </summary>
    public Exception? Error { get; private set; }
}