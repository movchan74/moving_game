using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public enum State
    {
        
        Idle,
        Active,
        Moving,
        Held,
        Completed
        
        
    }

    public Vector3 HoldOffset;
    public Vector3 HoldRotationOffset;

    public EffectController.EffectState EffectState = EffectController.EffectState.Normal;

    public State state;

    public float MoveDur = 1f;
    public AnimationCurve MoveCurve = AnimationCurve.EaseInOut(0,0,1,1);
    
    private Rigidbody rb;

    private Timer timer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        timer = Timer.AddTimer(gameObject);
        timer.TimerComplete += TimerOnTimerComplete;
    }

    private void TimerOnTimerComplete()
    {
        SetHeld();
        
    }

    void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            
                HitBox();
        }
    }


    public void Reset()
    {
        SetIdle();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case (State.Moving):
                UpdateMoving();
                break;
            case (State.Held):
                UpdateHeld();
                break;
        }
    }

    private void SetIdle()
    {
        state = State.Idle;
        rb.isKinematic = true;
    }
    
    void SetActive()
    {
        transform.parent = null;
       state = State.Active;
       rb.isKinematic = false;
    }

    private Vector3 startMovePos;
    private Quaternion startRot;
    
    void SetMoving()
    {
        state = State.Moving;
        timer.Reset(MoveDur);
        rb.isKinematic = true;
        transform.parent = ObjectHandler.Inst.ObjectHoldTransform;
        startMovePos = transform.localPosition;
        startRot = transform.localRotation;

    }

    void SetHeld()
    {
        
        state = State.Held;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        EffectController.Inst.SetMask(EffectState);
        
        
    }

    void SetCompleted()
    {
        state = State.Completed;
    }
    
    
    void UpdateMoving()
    {
        var r = MoveCurve.Evaluate(timer.Ratio());
        transform.localPosition =
            Vector3.Lerp(startMovePos, HoldOffset,r);

        transform.localRotation = Quaternion.Lerp(startRot, Quaternion.Euler(HoldRotationOffset), r );
    }

    private void UpdateHeld()    
    {                            
        transform.localPosition = HoldOffset;
        transform.localRotation = Quaternion.Euler(HoldRotationOffset);
    }                            
    public void Release()
    {
        EffectController.Inst.SetMask(EffectController.EffectState.Normal);
        SetActive();
        timer.Stop();
    }

    public void Pickup()
    {
        
        SetMoving();
    }

    void HitBox()
    {
        if (state != State.Completed)
        {
            SetCompleted();
            LevelStateHandler.Inst.ObjectCompleted();
        }
       
    }
    
    
    
    
    
    
    
    
    
    
}
