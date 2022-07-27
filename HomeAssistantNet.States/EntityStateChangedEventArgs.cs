using HomeAssistantNet.Api;

namespace HomeAssistantNet.States;

public class EntityStateChangedEventArgs : EventArgs
{
    internal EntityStateChangedEventArgs(HaEntityState? old, HaEntityState? @new)
    {
        Old = old;
        New = @new;
    }

    public HaEntityState? Old { get; private set; }
    public HaEntityState? New { get; private set; }
}