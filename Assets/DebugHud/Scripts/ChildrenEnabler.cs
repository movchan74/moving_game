using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenEnabler : MonoBehaviour {

	public List<GameObject> ObjectsToEnable = new List<GameObject>();

	private void Awake()
	{
		
		foreach (var o in ObjectsToEnable)
			o.SetActive(true);
	}
}
