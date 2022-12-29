namespace HomeAssistantNet.Client;

/// <summary>
/// Helper to generate valid options for websocket connection
/// </summary>
public sealed class HaClientOptionsBuilder
{
    readonly HaClientOptions options = new();

    /// <summary>
    /// Builds a options object to be used in a websocket connection
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public HaClientOptions Build()
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
        if (options.DisconnectTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.DisconnectTimeout), options.DisconnectTimeout,
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

    public HaClientOptionsBuilder WithHost(string host)
    {
        options.Host = host;
        return this;
    }

    public HaClientOptionsBuilder WithPort(int port)
    {
        options.Port = port;
        return this;
    }

    public HaClientOptionsBuilder WithWss()
    {
        options.UseWss = true;
        return this;
    }

    public HaClientOptionsBuilder WithConnectTimeout(TimeSpan timeout)
    {

        options.ConnectTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithDisconnectTimeout(TimeSpan timeout)
    {
        options.ConnectTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithReconnectMinTimeout(TimeSpan timeout)
    {
        options.ReconnectMinTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithReconnectMaxTimeout(TimeSpan timeout)
    {
        options.ReconnectMaxTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithSendTimeout(TimeSpan timeout)
    {

        options.SendTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithReceiveTimeout(TimeSpan timeout)
    {
        options.ReceiveTimeout = timeout;
        return this;
    }

    public HaClientOptionsBuilder WithTimeout(TimeSpan timeout)
    {
        return
            WithSendTimeout(timeout)
            .WithReceiveTimeout(timeout)
            .WithConnectTimeout(timeout)
            .WithDisconnectTimeout(timeout);
    }
    public HaClientOptionsBuilder WithReconnectTimeout(TimeSpan timeout)
    {
        return
            WithReconnectMinTimeout(timeout)
            .WithReconnectMaxTimeout(timeout);
    }

    public HaClientOptionsBuilder WithToken(string token)
    {
        options.Token = token;
        return this;
    }

    

}
