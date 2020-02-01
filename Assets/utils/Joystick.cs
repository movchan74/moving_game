using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour 
{
    public RectTransform ball1,ball2;
    public Vector2 startPos;
   // public float MaxDist = 2;

   public float MaxMoveChangeMag = 100f;
   
    public Vector2 MoveVecNormal;

    public static Joystick Inst;

    public bool ShowBalls = true;


    
    
     Canvas canvas;

    private void Awake()
    {
        Inst = this;
        InputListener.TouchScreen += InputListener_TouchScreen;
        canvas = GetComponent<Canvas>();
      
    }

    void InputListener_TouchScreen(Vector3 obj)
    {
        startPos = GetPoint(InputListener.InputViewportPosition);
        prevPoint =  GetPoint(InputListener.InputViewportPosition);
    }

    public void ResetJoystickPos()
    {
        startPos = GetPoint(InputListener.InputViewportPosition);
    }
    
    
    public Vector2 vec;
    private bool camExists = false;
    public Vector2 MoveChange;
    private Vector2 prevPoint;
    private void FixedUpdate()
    {
        //startPos = GetPoint(InputListener.InputViewportPositionStart);
        if (DataHolder.Data.ShowJoystick)
        {
            ball1.gameObject.SetActive(InputListener.mouseDown);
            ball2.gameObject.SetActive(InputListener.mouseDown);
        }
        else
        {
            ball1.gameObject.SetActive(false);
            ball2.gameObject.SetActive(false);
        }


        float MaxDist = DataHolder.Data.JoystickSize;

        if (InputListener.mouseDown)
        {
            Vector2 endPos = GetPoint(InputListener.InputViewportPosition);

           vec = endPos - startPos;
            if (vec.magnitude > MaxDist)
            {
                startPos = endPos - vec.normalized * MaxDist;
                vec = endPos - startPos;
            }
            ball1.anchoredPosition = startPos;
            ball2.anchoredPosition = endPos;

            MoveVecNormal = vec / MaxDist;
        }
        else
        {
            MoveVecNormal = Vector2.zero;
        }

        Vector2 curPoint = GetPoint(InputListener.InputViewportPosition);
        MoveChange = (curPoint - prevPoint);
        if (MoveChange.magnitude > MaxMoveChangeMag)
            MoveChange = MoveChange.normalized * MaxMoveChangeMag;
        prevPoint = curPoint;

        
    }

    public Vector2 GetVecNormal(float _maxDist)
    {
        Vector2 vecNormal = Vector2.zero;
        Vector2 _vec;
        Vector2 _startPos = startPos;
        
        
        if (InputListener.mouseDown)
        {
            Vector2 endPos = GetPoint(InputListener.InputViewportPosition);

            _vec = endPos - startPos;
            if (_vec.magnitude > _maxDist)
            {
                _startPos = endPos - _vec.normalized * _maxDist;
                _vec = endPos - startPos;
            }
           

            vecNormal = _vec / _maxDist;
        }

        return vecNormal;

    }

    
    
    Vector2 GetPoint(Vector3 viewportPoint)
    {
        //canvas = GetComponent<Canvas>();
        Vector2 sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        return new Vector2(viewportPoint.x * sizeDelta.x, viewportPoint.y * sizeDelta.y);
    }

    private void OnDestroy()
    {
        InputListener.TouchScreen -= InputListener_TouchScreen;
    }
    
    





}


