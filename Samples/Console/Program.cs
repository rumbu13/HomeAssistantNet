using HomeAssistantNet;
using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;
using HomeAssistantNet.Tools;
using Microsoft.Extensions.Logging;
using System.Text.Json;




Console.WriteLine("Welcome!");

var options = new HaClientOptionsBuilder()
    .WithHost("ha")
    .WithToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJjNGFmZmVlNDJkNmQ0MTA1YTcwNDk5MzYzNzQ1YWFjMiIsImlhdCI6MTY1OTY0ODAwMCwiZXhwIjoxOTc1MDA4MDAwfQ.oW_lwhowDq_FLmCUH2_gCothHEE9s1tXEBw21Mr-6hY")
    .Build();

IHaClient client2 = IHaClient.CreateDefault();



client2.Connecting += (s, a) => Console.WriteLine($"connecting attempt: {a.Attempt}");
client2.Connected += (s, a) =>
{
    Console.WriteLine($"connected {a.Version}");
    
};
client2.Disconnected += (s, a) => Console.WriteLine($"disconnected {a.DisconnectReason} - {a.Error?.Message}");
client2.EventReceived += (s, m) =>
{
    if (m.Event is HaStateChangedEvent)
        return;

    if (m.Event is HaServiceRegisteredEvent ev)
        Console.WriteLine($"message: {m.SubscriptionId}, {ev.EventType} - {m.Event.GetType()} - {ev.Data?.Domain}.{ev.Data?.Service}");
    else if (m.Event is HaComponentLoadedEvent cl)
        Console.WriteLine($"message: {m.SubscriptionId}, {cl.EventType} - {m.Event.GetType()} - {cl.Data?.Component}");
    else if (m.Event is HaEntitiesUpdatedEvent eu)
        Console.WriteLine($"message: {m.SubscriptionId}, {eu.EventType} - {m.Event.GetType()} - {eu.Data?.Action} - {eu.Data?.EntityId}");
    else if (m.Event is HaCallServiceEvent cs)
        Console.WriteLine($"message: {m.SubscriptionId}, {cs.EventType} - {m.Event.GetType()} - {cs.Data?.Domain}.{cs.Data?.Service}");
    else if (m.Event is HaStandardEvent ue)
        Console.WriteLine($"message: {m.SubscriptionId}, {ue.EventType} - {m.Event.GetType()}");
    else
        Console.WriteLine($"message: {m.SubscriptionId}, {m.Event.GetType()}");
};

await client2.StartAsync(options);
await client2.SubscribeEventsAsync(null, CancellationToken.None);

var devs = await client2.GetDevicesAsync();
var services = await client2.GetServicesAsync();
var entries = await client2.GetConfigEntriesAsync();
var es = await client2.GetEnergyPreferencesAsync();

var alltrigs = new List<HaDeviceTrigger>();
foreach (var dev in devs!)
{
    var trigs = await client2.GetDeviceTriggersAsync(dev.Id!);
    alltrigs.AddRange(trigs!);
}

Console.ReadKey();
client2.Dispose();


