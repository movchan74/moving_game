using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBInterface : MonoBehaviour
{
    public float HorizontalDrag = 0.1f;

    
    public float VerticalDrag = 0;
    public float GravAdd = 20;
    private Rigidbody rb;

    public float MaxVel = 100;

    public float LinearDrag = 0;
    public float AngularDrag = 0;

    public float Mass = 1;

    public float MaxHorVel = 200;
    public float MaxVerVel = 200;
    public float MaxClimbVel = 10;

    public float MaxForwardSpeed = 50;
    public float MinForwardSpeed = 50;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        if (rb.IsSleeping())
            return;
        
        //Vector2 joyVec = Joystick.Inst.MoveVecNormal;
        
        //Vector3 PushVec = new Vector3(Mathf.Clamp(joyVec.x,-1,1),0,Mathf.Clamp(joyVec.y,-1,1));

        //BallRB.AddForce(PushVec * PushForce * Time.fixedDeltaTime * DataHolder.Params.StepFactor, ForceMode.Acceleration);

        rb.AddForce(-Vector3.up * GravAdd * Time.fixedDeltaTime, ForceMode.VelocityChange);
        
        Vector3 vel = rb.velocity;
       vel.y = 0;
//        vel -= vel * HorizontalDrag;
//        vel.y = BallRB.velocity.y;
        rb.AddForce(-vel * HorizontalDrag*Time.fixedDeltaTime,ForceMode.VelocityChange);

        vel = rb.velocity;
        vel.z = 0;
        rb.AddForce(-vel * VerticalDrag*Time.fixedDeltaTime, ForceMode.VelocityChange);
        
        
        
        if (rb.velocity.magnitude > MaxVel)
            rb.velocity = rb.velocity.normalized * MaxVel;

        rb.angularDrag = AngularDrag;
        rb.drag = LinearDrag;
        rb.mass = Mass;

        vel = rb.velocity;
        if (Mathf.Abs(vel.y) > MaxVerVel)
            vel.y = MaxVerVel * Mathf.Sign(vel.y);

        if (vel.y > MaxClimbVel)
            vel.y = MaxClimbVel;

        if (vel.z > MaxForwardSpeed)
            vel.z = MaxForwardSpeed;
        
        var hVel = vel;
        hVel.y = 0;
        if (hVel.magnitude > MaxHorVel)
        {
            hVel = hVel.normalized * MaxHorVel;
        }

        vel.x = hVel.x;
        vel.z = hVel.z;

        if (vel.z < MinForwardSpeed)
            vel.z = MinForwardSpeed;
        
        rb.velocity = vel;


        

    }

    public void SetAllMaxVels(float allMax)
    {
        MaxClimbVel = allMax;
        
        MaxVel = allMax;
        MaxHorVel = allMax;
        MaxVerVel = allMax;
        MaxClimbVel = allMax;
        MaxForwardSpeed = allMax;
    }
    
}
