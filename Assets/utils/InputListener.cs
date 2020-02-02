using UnityEngine;
using System.Collections;
using System;


public class InputListener : MonoBehaviour
{

	public static event Action <Vector3> TouchScreen = v3 => {};
	public static event Action <Vector3> ReleaseScreen = v3 => {};
	public static event Action <Vector3> HoldScreen = v3 => {};
	public static bool mouseDown;
	public static Vector3 InputWorldPosition;
	public static Vector3 InputViewportPosition;
	public static Vector3 InputWorldPositionChange;
	public static Vector3 InputViewportPositionChange;
	public static Vector3 InputWorldPositionStart;
	public static Vector3 InputViewportPositionStart;

    public Camera camera;

   

	void Awake ()
	{
        camera = Camera.main;
        if (camera == null)
            camera = FindObjectOfType<Camera>();
	}

	public void Reset()
	{
		
		mouseDown = false;

	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
			TouchScreen (Input.mousePosition);
		
//
//		#if UNITY_EDITOR
//		UpdateMouseInput ();
//
//		#else
//		UpdateTouchInput();
//
//		#endif
	}

	void UpdateMouseInput ()
	{
		

		Vector3 touchPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10);
        //Debug.Log (touchPosition);
        TouchPosition = touchPosition;
		Vector3 touchWorldPosition = camera.ScreenToWorldPoint (touchPosition);

		Vector3 touchViewportPosition = camera.ScreenToViewportPoint (touchPosition);
		//Debug.Log (touchWorldPosition);
		InputWorldPosition = touchWorldPosition;
		InputViewportPosition = touchViewportPosition;

		//Debug.Log (InputWorldPosition);
		//Vector3 mouseSpeed = Input.mousePosition - prevMousePosition;

		if (Input.GetMouseButtonDown (0))
		{
			InputWorldPositionStart = InputWorldPosition;
			InputViewportPositionStart = InputViewportPosition;
			InputWorldPositionChange = Vector2.zero;
			InputViewportPositionChange = Vector2.zero;

			TouchScreen (Input.mousePosition);
			mouseDown = true;
		} else if (Input.GetMouseButtonUp (0))
		{
			InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;
			InputViewportPositionStart = InputViewportPosition - InputViewportPositionStart;

			
			ReleaseScreen (Input.mousePosition);	
			mouseDown = false;




		} else if (Input.GetMouseButton (0))
		{
			InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;
			InputViewportPositionChange = InputViewportPosition - InputViewportPositionStart;
			InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;	
			HoldScreen (Input.mousePosition);	
		}

		
		
	}

	public static Vector3 TouchPosition;
	void UpdateTouchInput ()
	{
		
		var touch = Input.GetTouch (0);
		
		Vector3 touchPosition = new Vector3 (touch.position.x, touch.position.y, 10);
		TouchPosition = touchPosition;
		Vector2 touchWorldPosition = camera.ScreenToWorldPoint (touchPosition);
		InputWorldPosition = touchWorldPosition;

		Vector3 touchViewportPosition = camera.ScreenToViewportPoint (touchPosition);
		InputViewportPosition = touchViewportPosition;

		if (Input.touches.Length == 1)
		{
			
			
			//Vector3 mouseSpeed = touchPosition - prevMousePosition;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					InputWorldPositionStart = InputWorldPosition;
					InputViewportPositionStart = InputViewportPosition;
					InputWorldPositionChange = Vector2.zero;
					InputViewportPositionChange = Vector2.zero;
					TouchScreen (touchPosition);
					mouseDown = true;
					break;
				case TouchPhase.Moved:
					InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;
					InputViewportPositionChange = InputViewportPosition - InputViewportPositionStart;
					HoldScreen (touchPosition);	
					break;
				case TouchPhase.Stationary:
					InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;
					InputViewportPositionChange = InputViewportPosition - InputViewportPositionStart;
					HoldScreen (touchPosition);	
					break;
				case TouchPhase.Ended:
					InputWorldPositionChange = InputWorldPosition - InputWorldPositionStart;
					InputViewportPositionChange = InputViewportPosition - InputViewportPositionStart;
					
					ReleaseScreen (touchPosition);
					mouseDown = false;
					break;
			}

			//prevMousePosition = touchPosition;

		}


	}



}


