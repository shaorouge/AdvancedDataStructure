using System;
using System.Collections.Generic;
using UnityEngine;

public class StepMovingState : IState
{
    private Controller controller;

    public StepMovingState(Controller ctrl)
    {
        controller = ctrl;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        controller.StepMove();
    }

    public void Exit()
    {
        controller.GetComponent<Renderer>().material.color = Color.white;
    }
}
