using System;
using System.Collections.Generic;

public interface IEventListener
{
    void Execute(IEvent e);
}
