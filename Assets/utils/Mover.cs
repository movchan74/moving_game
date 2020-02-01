using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverActivator))]
public class Mover : MonoBehaviour
{
    
    private void OnEnable()
    {
        //StartGameplayPhase();
    }

    public virtual void Reset()
    {
        
    }

    public virtual void UpdatePos(float ratio)
    {
        
    }

    public virtual void TimerCompleted()
    {
        
    }

    
}
