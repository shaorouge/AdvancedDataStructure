using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float Threshold { get; set; }
    private float defaultThreshold;
    private float current = 0.0f;

    public Timer() { }

    public Timer(float thd)
    {
        defaultThreshold = thd;
    }

    public bool UpdateAndCheck()
    {
        return UpdateAndCheck(defaultThreshold);
    }

    public bool UpdateAndCheck(float threshold)
    {
        current += Time.deltaTime;

        return current >= threshold;
    }

    public void Reset()
    {
        current = 0.0f;
    }


}
