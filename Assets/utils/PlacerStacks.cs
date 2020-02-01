using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class PlacerStacks : ObjectPlacerBase 
{
	public Vector3Int Size;
	public Vector3 Margins = new Vector2(3,3);
	public Vector3 MarginsMod;
	public Vector3 Offset = new Vector3(0,0.5f,0);
	public Vector3 ScaleYChange = Vector3.zero;
	public Vector3 RotYChange = Vector3.zero;
	
	public override void PlaceObjects()
	{
		base.PlaceObjects();

		if (PrefabList.Count == 0)
			return;
		
		Vector3 size = new Vector3((float)(Size.x-1) * Margins.x,
			(float)(Size.y-1) * Margins.y,(float)(Size.z-1) * Margins.z );

		int count = 0;
		for (int i = 0; i < Size.x; i++)
		{
			for (int j = 0; j < Size.y; j++)
			{
				for (int k = 0; k < Size.z; k++)
				{
					GameObject prefab;
					
					if (SelectMethod == PrefabSelectionMethod.Sequential)
					{
						prefab = PrefabList[count % PrefabList.Count];
						count++;
					}
					else
					{
						prefab = PrefabList.GetRandom();
					}
					
					Vector3 pos = new Vector3(-size.x * 0.5f + Margins.x * i, -size.y * 0.5f + Margins.y * j, -size.z * 0.5f  + Margins.z * k) + 
					              new Vector3((size.x + prefab.transform.localScale.x) * Offset.x, 
						              (size.y + prefab.transform.localScale.y) * Offset.y,
						              (size.z + prefab.transform.localScale.z) * Offset.z);
					GameObject thing = Utils.SpawnObject(prefab, transform, false);
					thing.transform.localPosition = pos;
					thing.transform.position += new Vector3(Utils.RandomMod(MarginsMod.x),Utils.RandomMod(MarginsMod.y),
						Utils.RandomMod(MarginsMod.z) );

					
					thing.transform.localScale += (float)j*ScaleYChange;
					thing.transform.Rotate((float)j*RotYChange);
					//thing.transform.localScale = Vector3.one;	
				}
		
			}
		}
		
	}
}
