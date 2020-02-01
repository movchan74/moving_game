using UnityEngine;
using System.Collections;
using System;

public class ColliderTriggerReporter3D : MonoBehaviour
{

	

	public event Action<GameObject, Collision> CollisionEnterEvent = (go1,col) => {};
    public event Action<GameObject, Collision> CollisionExitEvent = (go1,col) => {};
    public event Action<GameObject,Collider> TriggerEnterEvent = (go1,col) => {};
	public event Action<GameObject, Collider> TriggerExitEvent = (go1,col) => {};
	public event Action<GameObject, Collider> TriggerStayEvent = (go1,col) => {};

	void OnCollisionEnter(Collision col)
	{
		
		CollisionEnterEvent (gameObject,col);
		
	}

	void OnCollisionExit(Collision col)
	{
		CollisionExitEvent (gameObject,col);
	}
		
	void OnTriggerExit (Collider col)
	{

      
		TriggerExitEvent (gameObject, col);
	}

	void OnTriggerEnter (Collider col)
	{

        //Vector3 point = col.ClosestPoint()
        TriggerEnterEvent(gameObject, col);
	}

	void OnTriggerStay (Collider col)
	{
		TriggerStayEvent (gameObject, col);
	}
		
}
