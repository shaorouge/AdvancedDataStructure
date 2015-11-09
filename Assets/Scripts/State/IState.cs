using System;
using System.Collections.Generic;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}
