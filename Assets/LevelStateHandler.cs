using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateHandler : MonoBehaviour
{
    public static Action<State> NewStateSetEvent = sss => { };
   

    public enum State
    {
        Beginning,
        SearchForObject,
        CompletedObject,
        Ending
    }

    public static State state;

    public State state1;
    
    public static LevelStateHandler Inst;
    
    public static int CurrentObjInt = 0;


    public int MaxObjects = 4;
    public Timer timer;

    private void Awake()
    {
        Inst = this;
        timer = Timer.AddTimer(gameObject);
        timer.TimerComplete += TimerOnTimerComplete;
    }

    private void TimerOnTimerComplete()
    {
        SetSearchForObject(); 
    }

    void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        SetBeginning();
    }
    
    void SetBeginning()
    {
        CurrentObjInt = 0;
        state = State.Beginning;
        NewStateSetEvent(state);
    }

    void SetSearchForObject()
    {
        state = State.SearchForObject;
        NewStateSetEvent(state);
    }

    void SetCompletedObject()
    {
       
        
        
    }

    void SetEnding()
    {
        state = State.Ending;
        NewStateSetEvent(state);
    }
    
    public void ObjectCompleted()
    {
        if (CurrentObjInt < MaxObjects - 1)
        {
            if (state == State.SearchForObject)
            {
                CurrentObjInt++;
                SetSearchForObject();
            }
        }
        else
        {
            SetEnding();
        }
       
    }

    public void TextCompleted()
    {
        SetSearchForObject();
    }

    private void Update()
    {
        state1 = state;
    }
}
