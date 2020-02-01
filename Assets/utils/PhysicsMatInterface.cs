using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsMatInterface : MonoBehaviour
{
    private List<Collider> colList = new List<Collider>();

    public float Friction, Bounce;

    public float NoMoveFriction = 1000000;
    
    public PhysicMaterialCombine FrictionCombine = PhysicMaterialCombine.Maximum;
    public PhysicMaterialCombine     BounceCombine = PhysicMaterialCombine.Minimum;
    
    private void Awake()
    {
        colList = GetComponentsInChildren<Collider>().ToList();
    }

    public void Reset()
    {
        colList.Clear();
        colList = GetComponentsInChildren<Collider>().ToList();
    }
    
    private void FixedUpdate()
    {
        foreach (var col in colList)
        {
            var material = col.material;
            material.bounciness = Bounce;
            material.staticFriction = Friction;
            material.dynamicFriction = Friction;
            material.bounceCombine = BounceCombine;
            material.frictionCombine = FrictionCombine;
            if (!InputListener.mouseDown)
            {
                material.staticFriction = NoMoveFriction;
                material.dynamicFriction = NoMoveFriction;
            }
        }

        
    }
    
    
}
