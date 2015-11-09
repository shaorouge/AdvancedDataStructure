using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    public delegate void Listener(IEvent e);

    private Dictionary<AppEventType, Listener> listenersMap;
    private static EventBus instance;
    
    private EventBus()
    {
        listenersMap = new Dictionary<AppEventType, Listener>();
    }

    public static EventBus GetInstance()
    {
        if (instance == null)
            instance = new EventBus();

        return instance;
    }

    public void Subscribe(AppEventType type, Listener listener)
    {
        if (!listenersMap.ContainsKey(type))
            listenersMap.Add(type, new Listener(listener));
        else
            listenersMap[type] += listener;
    }

    public void Unsubscribe(AppEventType type, Listener listener)
    {
        if(!listenersMap.ContainsKey(type))
        {
            Debug.LogError(type.ToString() + " doesn't exist");
            return;
        }

        listenersMap[type] -= listener;
    }

    public void Trigger(IEvent e)
    {
        listenersMap[e.Type](e);
    }
}