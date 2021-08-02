using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float timerMax;
    float timerHand = 0;

    public Timer(float time)
    {
        timerMax = time;
        timerHand = timerMax;
    }

    public void Tick()
    {
        timerHand -= Time.deltaTime;
    }

    public void Tick(float dt)
    {
        timerHand -= dt;
    }

    public void Reset()
    {
        timerHand = timerMax;
    }

    public void Reset(float time)
    {
        timerMax = time;
        timerHand = timerMax;
    }

    public bool isDone()
    {
        return timerHand <= 0;
    }

    public float GetHand()
    {
        return timerHand;
    }
}
