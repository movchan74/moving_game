using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoverActivator))]
public class Positioner : Mover
{
    Vector3 origPos;
    Vector3 basePos;
    public Vector3 StartPos, EndPos, PosRange;
    public AnimationCurve curve;

    public bool ReverseCurveY = false;
   // private Rigidbody BallRB;
    
    
    public override void Reset()
    {
        
        origPos = transform.localPosition;
        basePos = origPos + Utils.RandomModVector(PosRange);
        //BallRB = GetComponent<Rigidbody>();
        
    }

    public override void UpdatePos(float ratio)
    {
        if (ReverseCurveY)
            ratio = 1 - ratio;
        
        //Debug.Log(ratio);
        
        Vector3 vec1 = basePos + StartPos;
        Vector3 vec2 = basePos + EndPos;
        Vector3 vec3 = Vector3.Lerp(vec1, vec2, curve.Evaluate(ratio));
 
        transform.localPosition = vec3;
        



    }

}
