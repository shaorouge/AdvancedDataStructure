using System;
using System.Collections.Generic;

public class ModifyButtonEvent : IEvent
{
    public AppEventType Type { get; set; }
    
    public ModifyButtonEvent(AppEventType type)
    {
        Type = type;
    }
}
