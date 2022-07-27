﻿namespace HomeAssistantNet.Client;

public class HaWsEventEventArgs : EventArgs
{
    internal HaWsEventEventArgs() { }

    internal HaWsEventEventArgs(HaEvent @event)
    {
        Event = @event;
    }

    public HaEvent? Event { get; internal set; }

}