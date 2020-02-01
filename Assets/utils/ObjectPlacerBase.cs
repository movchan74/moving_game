using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectPlacerBase : MonoBehaviour
{
	public List<GameObject> PrefabList;

	public bool PlaceOnStartGameplay = false;
	public bool AlwaysActive = false;
	public enum PrefabSelectionMethod
	{
		Sequential,
		Random
	}

	public PrefabSelectionMethod SelectMethod = PrefabSelectionMethod.Sequential;
	public bool Active = true;

//	private void OnEnable()
//	{
//		if (Application.isPlaying)
//		{
//			if (PlaceOnStartGameplay)
//				PlaceObjects();
//		}
//		
//	}

	protected  virtual  void Awake()
	{
		if (!Application.isPlaying)
		{
			
			Update();
			
			
		}
		else
		{
			if (PlaceOnStartGameplay)
			PlaceObjects();
		}
		
	}
	
	protected  virtual  void Update()
	{
		
		
		if (!Active && !AlwaysActive)
			return;
		
		if (!Application.isPlaying)
		{
			Active = false;
			PlaceObjects();
		}	
	}

	public virtual void PlaceObjects()
	{
		ClearObjects();
	}
	
	protected void ClearObjects()
	{
		var list = transform.GetChildrenList();

		foreach (var thing in list)
		{
			DestroyImmediate(thing.gameObject);
		}
	}

	protected int objectsCreatedCount;
	
	protected virtual GameObject GetPrefab()
	{
		GameObject prefab;
					
		if (SelectMethod == PrefabSelectionMethod.Sequential)
		{
			prefab = PrefabList[objectsCreatedCount % PrefabList.Count];
			objectsCreatedCount++;
		}
		else
		{
			prefab = PrefabList.GetRandom();
		}

		return prefab;
	}
}
