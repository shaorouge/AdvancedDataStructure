using System;
using System.Collections.Generic;
using UnityEngine;

public class AutoPushingState : IState
{
    private Manager manager;
    private System.Random random;

    public AutoPushingState(Manager m)
    {
        manager = m;
        random = new System.Random();
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        if(manager.heap.IsFull())
        {
            EventBus.GetInstance().Trigger(new ModifyButtonEvent(AppEventType.ENABLE_BUTTON));
            manager.SwitchState(StateEnum.MANUAL);
            return;
        }

        if(manager.heap.CanModify())
            manager.PushNumber(random.Next(0, 100));
    }

    public void Exit()
    {

    }
}
