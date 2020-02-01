using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action TimerComplete = () => { };

    public float duration = 0;
    public float PauseDuration = 0;
    public bool Repeat = false;
    float startTime;

    public enum State
    {
        Active,
        Stopped,
        Paused
    }

    public State state = State.Stopped;

    public static Timer AddTimer(GameObject tar, string timerName = "timer")
    {

        var go = new GameObject {name = timerName};
        go.transform.SetParent(tar.transform);
        Timer timer = go.AddComponent<Timer>();
       

        return timer;
    }
    
    public float Ratio()
    {


        if (duration == 0)
            return 0;

        if (state == State.Stopped)
            return 0;

        if (state == State.Paused)
            return pauseRatio;

        return Mathf.Clamp01((Time.time - startTime) / duration);
    }

    public void Stop()
    {
        duration = 0;
        state = State.Stopped;
    }

    public bool Active()
    {
        if (state != State.Stopped)
            return true;

        if (Ratio() > 0 && Ratio() < 1)
            return true;

        return false;
    }

    public void Reset(float _duration = -1, float autoCompletePercent = 0, bool _repeat = false)
    {
        if (_duration != -1)
            duration = _duration;

        Repeat = _repeat;

        startTime = Time.time - autoCompletePercent * duration;

        if (duration != 0)
            state = State.Active;
    }

    void Update()
    {
        if (state == State.Stopped)
            return;

        if (duration == 0)
        {
            state = State.Stopped;
            return;
        }

        if (state == State.Paused)
        {
            if (PauseDuration > 0)
            {
                if (Time.time - pauseStartTime > PauseDuration)
                {
                    Resume();
                }
            }
        }
            

        if (Ratio() >= 1)
        {

            if (Repeat)
                Reset(duration, 0, Repeat);
            else
            {
                state = State.Stopped;
                duration = 0;
            }

            TimerComplete();
        }

    }

    float pauseStartTime = 0;
    float pauseRatio;

    public void Pause(float _duration = 0)
    {
        if (_duration <= 0)
            PauseDuration = 0;
        else
            PauseDuration = _duration;

        pauseStartTime = Time.time;
        pauseRatio = Ratio();
        state = State.Paused;
    }

    public void Resume()
    {
        Reset(duration, pauseRatio, Repeat);
    }

    private float currentFactor = 1;

    private float origDuration;
    public void ChangeSpeed(float newFactor)
    {
        newFactor = Mathf.Max(0.0001f, newFactor);
        
        
        if (newFactor != currentFactor)
        {
            if (currentFactor == 1)
                origDuration = duration;
            currentFactor = newFactor;
        }
            
        
        Reset(origDuration/currentFactor,Ratio(),Repeat);
    }


}
