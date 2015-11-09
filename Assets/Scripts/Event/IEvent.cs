using System;
using System.Collections.Generic;

public interface IEvent
{
    AppEventType Type{get; set;}
}

public enum AppEventType
{
    SEND_NUMBER,
    SORT_NUMBER,
    DISABLE_BUTTON,
    ENABLE_BUTTON,
    DISABLE_OBJECT,
    ENABLE_OBJECT,
    IS_MOVABLE
}