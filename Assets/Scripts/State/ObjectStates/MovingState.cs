using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState
{
    private Controller controller;

    public MovingState(Controller ctrl)
    {
        controller = ctrl;
    }

    public void Enter()
    {
        if (controller.destination == controller.transform.position)
            Debug.LogError(controller.gameObject.ToString() + " doesn't need to move!");
    }

    public void Execute()
    {
        controller.Move();
    }

    public void Exit()
    {

    }
}
