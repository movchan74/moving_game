using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using HyperCasual.Extensions;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectPlacer : MonoBehaviour
{
	public GameObject prefab;

	public Vector3Int Size;
	public Vector3 Margins = new Vector2(3,3);
	public Vector3 MarginsMod = new Vector3(1,1,1);
	public Vector3 Offset = new Vector3(0,0.5f,0);

	public Vector2 StepYChange;
	public bool Active = true;
	
	private void Awake()
	{
		if (!Application.isPlaying)
		{
			
				Update();
			
			
		}
		else
		{
			PlaceObjects();
		}
		
	}

	public void Reset()
	{
		
	}

	private void Update()
	{
		if (!Active)
			return;
		if (!Application.isPlaying)
		{
			PlaceObjects();
		}	
	}

	public void PlaceObjects()
	{
		ClearObjects();
		Vector3 size = new Vector3((float)(Size.x-1) * Margins.x,
			(float)(Size.y-1) * Margins.y,(float)(Size.z-1) * Margins.z );
			
		
		for (int i = 0; i < Size.x; i++)
		{
			for (int j = 0; j < Size.y; j++)
			{
				for (int k = 0; k < Size.z; k++)
				{
					Vector3 pos = new Vector3(-size.x * 0.5f + Margins.x * i, -size.y * 0.5f + Margins.y * j, -size.z * 0.5f  + Margins.z * k) + 
						new Vector3((size.x + prefab.transform.localScale.x) * Offset.x, 
							(size.y + prefab.transform.localScale.y) * Offset.y,
							(size.z + prefab.transform.localScale.z) * Offset.z);
					GameObject thing = Utils.SpawnObject(prefab, transform, false);
					thing.transform.localPosition = pos;
					thing.transform.position += new Vector3(Utils.RandomMod(MarginsMod.x),Utils.RandomMod(MarginsMod.y),
						Utils.RandomMod(MarginsMod.z) );

					//thing.transform.localScale = Vector3.one;
					
					if (!Application.isPlaying)
					{
//						RandomTransform randTrans = thing.GetComponent<RandomTransform>();
//						if (randTrans != null)
//						{
//							randTrans.Init();
//							randTrans.Randomize();
//						}
					}
				}
				
				
			
			}
		}
	}

	void ClearObjects()
	{
		var list = transform.GetChildrenList();

		foreach (var thing in list)
		{
			DestroyImmediate(thing.gameObject);
		}
	}
}
