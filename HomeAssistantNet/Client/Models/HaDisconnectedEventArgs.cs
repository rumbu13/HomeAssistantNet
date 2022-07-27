namespace HomeAssistantNet.Client;

public sealed class HaDisconnectedEventArgs : EventArgs
{
    public HaDisconnectedEventArgs(bool reconnect, HaDisconnectReason disconnectReason, Exception? error)
    {
        Reconnect = reconnect;
        DisconnectReason = disconnectReason;
        Error = error;
    }

    public bool Reconnect { get; private set; }
    public HaDisconnectReason DisconnectReason { get; private set; }
    public Exception? Error { get; private set; }
}