using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEvent : IEvent
{
    public AppEventType Type { get; set; }
    public GameObject gameObject;

    public GameObjectEvent(AppEventType type, GameObject g)
    {
        Type = type;
        gameObject = g;
    }
}
