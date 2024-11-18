using HomeAssistantNet;
using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;
using HomeAssistantNet.Tools;
using Microsoft.Extensions.Logging;
using System.Text.Json;




Console.WriteLine("Welcome!");

var options = new HaClientOptionsBuilder()
    .WithHost("ha.pan")
    .WithToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJkZTA3OTI3ODM3Yjc0ODU1YjQxMGJjM2NiYmFjOGU0MyIsImlhdCI6MTczMTA0ODMzNywiZXhwIjoyMDQ2NDA4MzM3fQ.bAdbnLMlVh8KUIH1VmqdEMeMWWRdscHSDVFdTQhRixM")
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
    if (m.Event is HaStateChangedEvent sc)
        Console.WriteLine($"message: {m.SubscriptionId}, {sc.EventType} - {sc.Data?.EntityId} from {sc.Data?.OldState?.State} from {sc.Data?.NewState?.State}");
    else if (m.Event is HaServiceRegisteredEvent ev)
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
}; client2.EventReceived += (s, m) =>
{
    if (m.Event is HaStateChangedEvent sc)
        Console.WriteLine($"message: {m.SubscriptionId}, {sc.EventType} - {sc.Data?.EntityId} from {sc.Data?.OldState?.State} from {sc.Data?.NewState?.State}");
    else if (m.Event is HaServiceRegisteredEvent ev)
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



//devices
try
{
    var devs = await client2.GetDevicesAsync();
    Console.WriteLine($"You have {devs?.Length ?? 0} devices");
    if (devs?.First() is HaDevice dev)
    {
        Console.WriteLine($"Here is the first one: ID: '{dev.Id}', Name: '{dev.Name}'\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured while enumerating devices: {ex.Message}\n");
}

//services
try
{
    var services = await client2.GetServicesAsync();
    Console.WriteLine($"You have {services?.Count ?? 0} services");
    var sample = services?.FirstOrDefault();
    if (sample is not null)
    {
        Console.WriteLine($"Here is the first one: Domain: '{sample.Value.Key}', Service: '{sample.Value.Value.FirstOrDefault().Key}'\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured while enumerating services: {ex.Message}\n");
}

//configuration entries
try
{
    var entries = await client2.GetConfigEntriesAsync();
    Console.WriteLine($"You have {entries?.Length ?? 0} configuration entries");    
    if (entries?.FirstOrDefault() is HaConfigEntry entry)
    {
        Console.WriteLine($"Here is the first one: ID: '{entry.EntryId}', Title: '{entry.Title}'\n");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occured while enumerating configuration entries: {ex.Message}\n");
}





try
{
    var es = await client2.GetEnergyPreferencesAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString()); 
}

//var alltrigs = new List<HaDeviceTrigger>();
//foreach (var dev in devs!)
//{
//    var trigs = await client2.GetDeviceTriggersAsync(dev.Id!);
//    alltrigs.AddRange(trigs!);
//}

//here we start to receive events

await client2.SubscribeToEventsAsync(null, CancellationToken.None);

Console.ReadKey();
client2.Dispose();


