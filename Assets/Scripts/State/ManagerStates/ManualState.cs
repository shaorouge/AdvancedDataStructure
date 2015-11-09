using System;
using System.Collections.Generic;

public class ManualState : IState
{
    private Manager manager;

    public ManualState(Manager m)
    {
        manager = m;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        if(manager.heap.CanModify())
            EventBus.GetInstance().Trigger(new ModifyButtonEvent(AppEventType.ENABLE_BUTTON));
    }

    public void Exit()
    {

    }
}
