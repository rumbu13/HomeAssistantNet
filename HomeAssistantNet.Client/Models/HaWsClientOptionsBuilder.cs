namespace HomeAssistantNet.Client;

/// <summary>
/// Helper to generate valid options for websocket connection
/// </summary>
public sealed class HaWsClientOptionsBuilder
{
    readonly HaWsClientOptions options = new();

    /// <summary>
    /// Builds a options object to be used in a websocket connection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public HaWsClientOptions Build()
    {
        if (string.IsNullOrWhiteSpace(options.Host))
            throw new ArgumentException("Invalid host name, cannot be empty.");
        if (string.IsNullOrWhiteSpace(options.Token))
            throw new ArgumentException("Invalid token, cannot be empty.");
        if (options.Port is < 0 or > 65535)
            throw new ArgumentOutOfRangeException(nameof(options.Port), options.Port, "Invalid port number");
        if (options.ConnectTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.ConnectTimeout), options.ConnectTimeout,
                "Invalid connect timeout");
        if (options.DiconnectTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.DiconnectTimeout), options.DiconnectTimeout,
                "Invalid disconnect timeout");
        if (options.SendTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.SendTimeout), options.SendTimeout,
                "Invalid send timeout");
        if (options.ReceiveTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.ReceiveTimeout), options.ReceiveTimeout,
                "Invalid receive timeout");
        if (options.ReconnectMinTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.ReconnectMinTimeout), options.ReconnectMinTimeout,
                "Invalid reconnect minimum timeout");
        if (options.ReconnectMaxTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.ReconnectMaxTimeout), options.ReconnectMaxTimeout,
                "Invalid reconnect maximum timeout");
        if (options.ReconnectMinTimeout > options.ReconnectMaxTimeout)
            throw new ArgumentException("Minimum reconnect timeout is greater than maximum reconnect timeout");

        return options;
    }

    public HaWsClientOptionsBuilder WithHost(string host)
    {
        options.Host = host;
        return this;
    }

    public HaWsClientOptionsBuilder WithPort(int port)
    {
        options.Port = port;
        return this;
    }

    public HaWsClientOptionsBuilder WithWss()
    {
        options.UseWss = true;
        return this;
    }

    public HaWsClientOptionsBuilder WithConnectTimeout(TimeSpan timeout)
    {

        options.ConnectTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithDisconnectTimeout(TimeSpan timeout)
    {
        options.ConnectTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithReconnectMinTimeout(TimeSpan timeout)
    {
        options.ReconnectMinTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithReconnectMaxTimeout(TimeSpan timeout)
    {
        options.ReconnectMaxTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithSendTimeout(TimeSpan timeout)
    {

        options.SendTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithReceiveTimeout(TimeSpan timeout)
    {
        options.ReceiveTimeout = timeout;
        return this;
    }

    public HaWsClientOptionsBuilder WithTimeout(TimeSpan timeout)
    {
        return
            WithSendTimeout(timeout)
            .WithReceiveTimeout(timeout)
            .WithConnectTimeout(timeout)
            .WithDisconnectTimeout(timeout);
    }
    public HaWsClientOptionsBuilder WithReconnectTimeout(TimeSpan timeout)
    {
        return
            WithReconnectMinTimeout(timeout)
            .WithReconnectMaxTimeout(timeout);
    }

    public HaWsClientOptionsBuilder WithToken(string token)
    {
        options.Token = token;
        return this;
    }


}
