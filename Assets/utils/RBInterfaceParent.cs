using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RBInterfaceParent : MonoBehaviour
{
    public float HorizontalDrag = 0.1f;

    public float GravAdd = 20;
    
    public float MaxVel = 100;

    public float LinearDrag = 0;
    public float AngularDrag = 0;

    public float Mass = 1;

    public float MaxHorVel = 200;
    public float MaxVerVel = 200;
    public float MaxClimbVel = 100;
    
    
    
    
    List<RBInterface> rbInterfaceList = new List<RBInterface>();

    private void Awake()
    {
        
        rbInterfaceList = GetComponentsInChildren<RBInterface>().ToList();
    }

    public void Reset()
    {
        rbInterfaceList.Clear();
        rbInterfaceList = GetComponentsInChildren<RBInterface>().ToList();

       
    }

    private void FixedUpdate()
    {
        foreach (var v in rbInterfaceList)
        {
            v.HorizontalDrag = HorizontalDrag;
            v.GravAdd = GravAdd;
            v.MaxVel = MaxVel;
            v.LinearDrag = LinearDrag;
            v.AngularDrag = AngularDrag;
            v.Mass = Mass;
            v.MaxHorVel = MaxHorVel;
            v.MaxVerVel = MaxVerVel;
            v.MaxClimbVel = MaxClimbVel;
           
        }

    }
}
