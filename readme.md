# .NET Home Assistant Client

.Net Library for controlling and communicating with Home Assistant through Websocket API.

## Basic usage

```csharp

//setting basic options
var options = new HaClientOptionsBuilder()
    .WithHost("homeassistant")
    .WithToken("<PUT YOUR TOKEN HERE>");

//create a client
var client = new IHaClient.CreateDefault();

//get a list of areas
var areas = await client.GetAreasAsync();

//get some states
var states = await client.GetStatesAsync();

//what happens when we receive something
client.EventReceived += (sender, args)
    => Console.WriteLine($"Event: {args.Event.EventType}");

//start the client
await client.StartAsync(options);

//subscribe to all events
await client.SubscribeToEventsAsync();




Console.ReadKey();
await client.StopAsync();

```


