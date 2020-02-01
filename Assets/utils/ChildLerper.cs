using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLerper : MonoBehaviour
{
    public Transform child;
    public float LerpFactor = 10;
    public float RotLerpFactor = 20;
     Vector3 curPos;
     private Vector3 curForward;
     public bool LerpRot;
     
     
    private void FixedUpdate()
    {
        var tarPos = transform.position;
        //curPos.z = tarPos.z;
       // curPos.x = tarPos.x;
        curPos = Vector3.Lerp(curPos, tarPos, LerpFactor * Time.fixedDeltaTime);
        
        child.transform.position =  curPos;

        if (LerpRot)
        {
            curForward = Vector3.Lerp(curForward, transform.forward, RotLerpFactor * Time.deltaTime);

            child.transform.forward = curForward;
        }
        else
        {

            child.transform.forward = transform.forward;
        }
      

    }


    public void Reset()
    {
        curPos = transform.position;
        curForward = transform.forward;
    }
}
