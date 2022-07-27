using HomeAssistantNet.Api;

namespace HomeAssistantNet.States;

public interface IHaStateManager
{
    IEnumerable<HaEntityState> GetStates();
    event EventHandler<EntityStateChangedEventArgs> StateChanged;
}