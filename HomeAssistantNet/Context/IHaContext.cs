using HomeAssistantNet.Api;

namespace HomeAssistantNet.Context;

public interface IHaContext
{
    Task WaitForLoadAsync();

    IEnumerable<HaArea> GetAreas();
    IEnumerable<HaDevice> GetDevices();
    IEnumerable<HaEntity> GetEntities();
    IEnumerable<HaConfigEntry> GetConfigEntries();
    IEnumerable<HaService> GetServices();
    IEnumerable<HaEntityState> GetEntityStates();
    IEnumerable<HaCounter> GetCounters();
    IEnumerable<HaInputBoolean> GetInputBooleans();

    HaArea? GetArea(string id);
    HaDevice? GetDevice(string id);
    HaEntity? GetEntity(string id);
    HaConfigEntry? GetConfigEntry(string id);
    HaService? GetService(string id);
    HaEntityState? GetEntityState(string id);
    HaCounter? GetCounter(string id);
    HaInputBoolean? GetInputBoolean(string id);


}
