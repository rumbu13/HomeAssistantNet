# .NET Home Assistant Client

.Net Library for controlling and communicating with Home Assistant. 
Includes REST API and Websocket API.

## Basic usage

```csharp

var options = new HaWsClientOptionsBuilder()
    .WithHost("homeassistant")
    .WithToken("<PUT YOUR TOKEN HERE>");

var client = new HaWsClient();

client.EventReceived += (sender, args)
    => Console.WriteLine($"Event: {args.Event.EventType}");

client.Start(options);

Console.ReadKey();
client.Stop();

```