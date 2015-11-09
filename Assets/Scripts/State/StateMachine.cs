using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected Dictionary<StateEnum, IState> stateMap;
    protected IState currentState;

    public void Execute()
    {
        currentState.Execute();
    }

    public void SwitchState(StateEnum nextStateEnum)
    {
        currentState.Exit();
        currentState = stateMap[nextStateEnum];
        currentState.Enter();
    }

    public IState GetState(StateEnum se)
    {
        if (stateMap.ContainsKey(se))
            return stateMap[se];

        Debug.LogError("Key doesn't exist");

        return null;
    }
}

public enum StateEnum
{
    IDLE,
    WAITING,
    MOVING,
    STEP_MOVING,

    AUTO_PUSHING,
    MANUAL
}