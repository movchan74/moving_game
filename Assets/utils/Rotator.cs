using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Mover 
{
    Vector3 origRot;
    Vector3 baseRot;
    public Vector3 StartRot, EndRot, RotRange;
    public bool RandomDir;

    private float dir = 1;
    
    public AnimationCurve curve;
    public override void Reset()
    {
        origRot = transform.localEulerAngles;
        baseRot = origRot + Utils.RandomModVector(RotRange);
        
        
        dir = 1;
        if (RandomDir)
        {
            if (Random.value < 0.5f)
                dir = -1;
        }
      
    }

    public override void UpdatePos(float ratio)
    {
        Vector3 vec1 = baseRot + StartRot*dir;
        Vector3 vec2 = baseRot + EndRot*dir;
        Vector3 vec3 = Vector3.Lerp(vec1, vec2, curve.Evaluate(ratio));
        transform.localEulerAngles = vec3;


    }
}
