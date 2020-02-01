using UnityEngine;
using System.Collections;
using System;

public class ColliderTriggerReporter2D : MonoBehaviour
{

	public static event Action<GameObject,GameObject> CollisionEnterEvent = (go1,go2) => {};
	public static event Action<GameObject,GameObject> CollisionExitEvent = (go1,go2) => {};
	//public static event Action<GameObject,Collision2D> CollisionStayEvent = (go1,go2) => {};

	public static event Action<GameObject,GameObject> TriggerEnterEvent = (go1,go2) => {};
	public static event Action<GameObject,GameObject> TriggerExitEvent = (go1,go2) => {};
	public static event Action<GameObject,GameObject> TriggerStayEvent = (go1,go2) => {};


	void OnCollisionEnter2D(Collision2D col)
	{
		CollisionEnterEvent (gameObject,col.collider.gameObject);
	}

	void OnCollisionExit2D(Collision2D col)
	{
		CollisionExitEvent (gameObject,col.collider.gameObject);
	}


	/*

	void OnCollisionStay2D (Collision2D col)
	{
		CollisionStayEvent (gameObject, col);
	}
	*/
	void OnTriggerExit2D (Collider2D col)
	{
		TriggerExitEvent (gameObject, col.gameObject);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		TriggerEnterEvent (gameObject, col.gameObject);
	}

	void OnTriggerStay2D (Collider2D col)
	{
		TriggerStayEvent (gameObject, col.gameObject);
	}





}
