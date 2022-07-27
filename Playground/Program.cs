using HomeAssistantNet;
using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Context;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//ClientWebSocket client = new ClientWebSocket();
//var uri = new Uri("ws://ha:8123/api/websocket");

//await client.ConnectAsync(uri, CancellationToken.None);

////client.ReceiveAsync()

var options = new HaWsClientOptionsBuilder()
    .WithHost("ha")
    .WithConnectTimeout(TimeSpan.FromSeconds(30))
    .WithTimeout(TimeSpan.FromSeconds(30))
    .WithToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJiYzVmNDg2MmQzMzE0OWQ1YjBlY2M3M2IzN2Y1ODkxYiIsImlhdCI6MTY1NzkwMDYzOSwiZXhwIjoxOTczMjYwNjM5fQ.lChYKdH5cFMOJnfsL8yfQtwXoIhezODjL7nMfKEbwQ8").Build();//await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "normal", CancellationToken.None);


var client = new HaWsClient();

client.EventReceived += (sender, args)
    => Console.WriteLine($"Event: {args.Event.EventType}");

client.Start(options);

Console.ReadKey();
client.Stop();



//var client = new HaWsClient();

//client.Start(options);


//var context = new HaContext(client);


//client.Connected += (s, e) => Console.WriteLine("Connected: " + e.Version);

//client.Disconnected += (s, e) => Console.WriteLine("Disconnected: " + e.DisconnectReason.ToString());

//client.MessageReceived += (s, e) => Console.WriteLine(e.Message);

//var context = new HaContext(client);

//bool cancel = false;

//Console.CancelKeyPress += (s, e) => cancel = true;


//var startTime = DateTime.Now;
//await context.WaitForLoadAsync();
//var elapsed = DateTime.Now - startTime;

//Console.WriteLine($"Elapsed for load: {elapsed.TotalSeconds:0.00}");

//var bools = context.GetInputBooleans();

//client.Start(options);

//await Task.Delay(TimeSpan.FromSeconds(3));

//var areas = context.GetAreas();
//var devices = context.GetDevices();

//using (StreamWriter file = new("test.cs"))
//{
//    file.WriteLine("using HomeAssitant;");
//    file.WriteLine("using HomeAssitant.Context;");
//    file.WriteLine("public class Areas");
//    file.WriteLine("{");
//    file.WriteLine("    private IHaContext context;");
//    file.WriteLine();
//    file.WriteLine("    public Areas(IHaContext context)");
//    file.WriteLine("    {");
//    file.WriteLine("        this.context = context");
//    file.WriteLine("    }");
//    file.WriteLine();


//    foreach (var area in areas)
//    {
//        file.WriteLine();
//        file.WriteLine($"    /// <summary>");
//        file.WriteLine($"    /// {area.Name}");
//        file.WriteLine($"    /// </summary>");
//        file.WriteLine($"    public Area {HaOptions.Prettify(area.AreaId)} => context.GetArea(\"{area.AreaId}\");");
//    }

//    file.WriteLine("}");
//    file.WriteLine();

//    file.WriteLine("public class Devices");
//    file.WriteLine("{");
//    file.WriteLine("    private IHaContext context;");
//    file.WriteLine();
//    file.WriteLine("    public Devices(IHaContext context)");
//    file.WriteLine("    {");
//    file.WriteLine("        this.context = context");
//    file.WriteLine("    }");
//    file.WriteLine();


//    foreach (var device in devices)
//    {
//        file.WriteLine();
//        file.WriteLine($"    /// <summary>");
//        file.WriteLine($"    /// {device.NameByUser ?? device.Name}");
//        file.WriteLine($"    /// </summary>");
//        file.WriteLine($"    public Device {HaOptions.Prettify(device.NameByUser ?? device.Name!)} => context.GetDevice(\"{device.DeviceId}\");");
//    }

//    file.WriteLine("}");

//}
//var services = await client.GetServicesAsync();
//var states = await client.GetStatesAsync();
//var areas = await client.GetAreasAsync();
//var devices = await client.GetDevicesAsync();
//var entities = await client.GetEntitiesExtendedAsync();
//var configEntries = await client.GetConfigEntriesAsync();


//var entitiesFromStates = states.Select(x => x.EntityId);
//var entitiesFromRegistry = entities.Select(x => x.EntityId);

//var statesExclusive = entitiesFromStates.Except(entitiesFromRegistry);
//var registryExclusive = entitiesFromRegistry.Except(entitiesFromStates);

//var allEntities = entitiesFromStates.Union(entitiesFromRegistry);

//var commonEntities = entitiesFromStates.Intersect(entitiesFromRegistry);

////var domainsFromServices = services.SelectMany(s => s.Value).Select(k => k.Value.Target.D);

//while (!cancel)
//{
//    await Task.Delay(1000);
//}

//var conn = new WebSocketConnection();
//var client = new WebSocketClient(conn);
//await client.StartAsync(options);