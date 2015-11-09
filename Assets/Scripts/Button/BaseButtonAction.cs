using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseButtonAction : MonoBehaviour, IEventListener
{
    private UIButton button;

    void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        EventBus.GetInstance().Subscribe(AppEventType.ENABLE_BUTTON, Execute);
        EventBus.GetInstance().Subscribe(AppEventType.DISABLE_BUTTON, Execute);

        if (GetComponent<UIButton>() != null)
            button = GetComponent<UIButton>();
        else
            Debug.LogError("No UIButton Component!");
    }

    public virtual void OnButtonClicked()
    {

    }

    public void Execute(IEvent e)
    {
        switch(e.Type)
        {
            case AppEventType.ENABLE_BUTTON: button.isEnabled = true; break;
            case AppEventType.DISABLE_BUTTON: button.isEnabled = false; break;
            default: Debug.LogError("Wrong type: " + e.Type); break;
        }
    }
}
