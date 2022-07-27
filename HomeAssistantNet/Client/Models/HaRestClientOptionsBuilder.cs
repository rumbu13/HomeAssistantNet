namespace HomeAssistantNet.Client;

public sealed class HaRestClientOptionsBuilder
{
    readonly HaRestClientOptions options = new();


    public HaRestClientOptions Build()
    {
        if (string.IsNullOrWhiteSpace(options.Host))
            throw new ArgumentException("Invalid host name, cannot be empty.");
        if (string.IsNullOrWhiteSpace(options.Token))
            throw new ArgumentException("Invalid token, cannot be empty.");
        if (options.Port is < 0 or > 65535)
            throw new ArgumentOutOfRangeException(nameof(options.Port), options.Port, "Invalid port number");
        if (options.SendTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.SendTimeout), options.SendTimeout,
                "Invalid send timeout");
        if (options.ReceiveTimeout < TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(options.ReceiveTimeout), options.ReceiveTimeout,
                "Invalid receive timeout");
        return options;
    }

    public HaRestClientOptionsBuilder WithHost(string host)
    {
        options.Host = host;
        return this;
    }

    public HaRestClientOptionsBuilder WithPort(int port)
    {

        options.Port = port;
        return this;
    }

    public HaRestClientOptionsBuilder WithHttps()
    {
        options.UseHttps = true;
        return this;
    }

    public HaRestClientOptionsBuilder WithSendTimeout(TimeSpan timeout)
    {

        options.SendTimeout = timeout;
        return this;
    }

    public HaRestClientOptionsBuilder WithReceiveTimeout(TimeSpan timeout)
    {
        options.ReceiveTimeout = timeout;
        return this;
    }

    public HaRestClientOptionsBuilder WithTimeout(TimeSpan timeout)
    {
        return
             WithSendTimeout(timeout)
            .WithReceiveTimeout(timeout);
    }

    public HaRestClientOptionsBuilder WithToken(string token)
    {
        options.Token = token;
        return this;
    }


}
