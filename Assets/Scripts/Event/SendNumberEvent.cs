using System;
using System.Collections.Generic;

public class SendNumberEvent : IEvent
{
    public AppEventType Type{get; set;}
    public int number;

    public SendNumberEvent(int value)
    {
        Type = AppEventType.SEND_NUMBER;
        number = value;
    }
}
