using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    public enum State
    {
        Free,
        Holding
    }

    public State state;
    
    public LayerMask RayLayer;

    public Image Pointer;
    public Color IdlePointerColor;
    public Color ActivePointerColor;

    public static ObjectHandler Inst;
    public Transform ObjectHoldTransform;
    public bool AnObjectCanBePickedUp;

    public PickupObject detectedObject;
    public PickupObject heldObject;

    public PickupObject desiredObject;
    
    public float TouchDist = 2;
    
    public List<PickupObject> DesiredObjList = new List<PickupObject>();
    
    private void Awake()
    {
        InputListener.TouchScreen += InputListenerOnTouchScreen;
        LevelStateHandler.NewStateSetEvent += NewStateSetEvent;
        Inst = this;
    }

    
    private void OnDestroy()
    {
        InputListener.TouchScreen -= InputListenerOnTouchScreen;
        LevelStateHandler.NewStateSetEvent -= NewStateSetEvent;
    }
    
    private void NewStateSetEvent(LevelStateHandler.State obj)
    {
        if (obj == LevelStateHandler.State.SearchForObject)
        {
            desiredObject = DesiredObjList[LevelStateHandler.CurrentObjInt];
        }
    }

    private void InputListenerOnTouchScreen(Vector3 obj)
    {
        if (LevelStateHandler.state != LevelStateHandler.State.SearchForObject)
            return;

        if (TextDisplayer.Inst.TextIsActive)
            return;
        
        
       
        
        if (state == State.Free)
        {
            if (AnObjectCanBePickedUp)
            {
                
                if (detectedObject == desiredObject)
                {
                    
                    heldObject = detectedObject;
                    heldObject.Pickup();
                    state = State.Holding;
                }
                
            }
        }
        else
        {
            heldObject.Release();
            state = State.Free;
        }
        
    }

    private bool IsLookingAtPickableObject() {
        return (AnObjectCanBePickedUp && detectedObject == desiredObject);
    }

    private void FixedUpdate()
    {
        if (state == State.Free)
            LookForObject();
        
        if (state == State.Free) {
            if (IsLookingAtPickableObject()) {
                Pointer.color = ActivePointerColor;
            }
            else {
                Pointer.color = IdlePointerColor;
            }
        }
        else {
            Pointer.color = Color.clear;
        }
    }

    void LookForObject()
    {
        var ray = GM.Inst.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        RaycastHit hit;

        var cast = Physics.Raycast(ray, out hit, TouchDist, RayLayer );

        AnObjectCanBePickedUp = cast;

        if (AnObjectCanBePickedUp)
        {
            var pickup = hit.collider.gameObject.GetComponentInParent<PickupObject>();
            if (pickup != null)
            {
                detectedObject = pickup;
            }
            else
            {
                AnObjectCanBePickedUp = false;
            }
            
        }
    }
    
    
}
