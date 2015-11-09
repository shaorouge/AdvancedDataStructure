using System;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : IState
{
    private Controller controller;
    private Timer timer;

    public WaitingState(Controller ctrl)
    {
        controller = ctrl;
        timer = new Timer();
    }

    public void Enter()
    {
        timer.Reset();
    }

    public void Execute()
    {
        if(timer.UpdateAndCheck(controller.component.duration))
            controller.SwitchState(StateEnum.MOVING);
    }

    public void Exit()
    {
        
    }
}
