using System;
using System.Collections.Generic;

public class ParamComponent
{
    public float Duration { get { return duration;} }
    public float Speed { get { return speed;} }

    public float duration;
    public float speed;

    public ParamComponent(float d, float s)
    {
        SetParams(d, s);
    }

    public void SetParams(float d, float s)
    {
        duration = d;
        speed = s;
    }
}
