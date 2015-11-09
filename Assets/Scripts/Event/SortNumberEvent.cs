using System;
using System.Collections.Generic;

public class SortNumberEvent : IEvent
{
    public AppEventType Type { get; set; }
    public Controller ctrl;

    public SortNumberEvent(Controller controller)
    {
        Type = AppEventType.SORT_NUMBER;
        ctrl = controller;
    }
}
