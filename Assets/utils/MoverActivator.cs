using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HerdIt.Interfaces;
using HyperCasual.Extensions;
using UnityEngine;

public class MoverActivator
    : MonoBehaviour, IResettable
{
    Timer timer;

    public enum State
    {
        Inactive,
        Active,
        Complete
    }
    
    public State state;
    
    public float MoveDuration = 2;
    public float MoveDurationRange = 0;
     public bool Repeat = false;
    public FloatRange StartDuration = new FloatRange(0, 0);
    List<Mover> moverList = new List<Mover>();
    List<GameObject> childrenList = new List<GameObject>();

    public Transform PlayerTar;
   
    private float currentStartDuration;

     float ActivationDist = 2000;
    public FloatRange ActivationDistRange = new FloatRange(500,1500);

    private bool hasReseted = false;

    private void GmOnChangeGameSpeed(float obj)
    {  
            timer.ChangeSpeed(obj); 
    }

    private void Start()
    {
      Reset();
        
    }

    public void Reset()
    {
        //PlayerTar = ShoeHolder.Inst.torso.transform;
        hasReseted = true;
        
        state = State.Inactive;
       

        timer = GetComponent<Timer>();
        
        if (timer == null)
            timer = gameObject.AddComponent<Timer>();

        timer.TimerComplete += Timer_TimerComplete;

        currentStartDuration = StartDuration.Random;        
        moverList = GetComponents<Mover>().ToList();
        
        foreach (Mover mover in moverList)
        {
            mover.Reset();
            mover.UpdatePos(currentStartDuration);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            childrenList.Add(child.gameObject);
        }

        ActivationDist = ActivationDistRange.Random;
        
        timer.Stop();
        /// Activate();  
    }
   
    public void Activate()
    {
        
        if (timer == null)
            return;
        
        if (state == State.Active || state == State.Complete)
            return;

        timer.Reset(MoveDuration + Utils.RandomMod(MoveDurationRange),currentStartDuration,Repeat);
        state = State.Active;
    }
    

    void Timer_TimerComplete()
    {
        
        foreach (Mover mover in moverList)
        {
            
            mover.UpdatePos(1);
        }

        if (!Repeat)
            state = State.Complete;
    }

    public void FixedUpdate()
    {
        if (!hasReseted)
            Reset();
        
        if (timer.Active())
        {
           
            foreach (Mover mover in moverList)
            {
                mover.UpdatePos(timer.Ratio());
            } 
        }
        else
        {
            if (state == State.Inactive)
            {
                
                if (transform.position.z - PlayerTar.position.z < ActivationDist)
                {
                    Activate();
                }
            }
           
        }
       
           
    }
    
    

   
}
