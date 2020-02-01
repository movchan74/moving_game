using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;



public class HudManager : MonoBehaviour 
{
	public event Action DebugOpenedEvent = () => {};
	public event Action DebugClosedEvent = () => {};

	public GameObject debugButton, debugDialog;
	DataHolder data;
	public DebugManager debugManager;
	bool debugActive = false;

    
	void Awake()
	{
		debugActive = false;
		data = FindObjectOfType <DataHolder> ();
		//debugManager = FindObjectOfType <DebugManager> ();
        
		debugManager.ResetGeneralData ();


		//debugManager 
	}

	

	void ActivateDebug()
	{
		//Debug.Log("openedDebug");
		debugDialog.SetActive(true);
		DebugOpenedEvent ();
		debugActive = true;
	}

	void DeactiveDebug()
	{
		//Debug.Log("closedDebug");
		debugDialog.SetActive(false);
		DebugClosedEvent ();
		debugActive = false;
	}

	

	public void DebugButtonClicked()
	{
		
		if (debugDialog.activeInHierarchy)
			DeactiveDebug ();
		else
			ActivateDebug ();

	}

	public void CloseDebugButtonClicked()
	{
		
	}



}

