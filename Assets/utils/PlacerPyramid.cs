using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual.Extensions;

[ExecuteInEditMode]
public class PlacerPyramid : ObjectPlacerBase 
{
	public Vector3Int Size;
	public Vector3 Margins = new Vector2(3,3);
	//public Vector3 MarginsMod;
	public Vector3 Offset = new Vector3(0,0.5f,0);
	public override void PlaceObjects()
	{
		base.PlaceObjects();

		if (PrefabList.Count == 0)
			return;
		
		int count = 0;
		for (int j = 0; j < Size.y; j++)
		{
			Vector3Int curSize = new Vector3Int(Size.x -j, Size.y,Size.z - j );
			for (int i = 0; i < curSize.x; i++)
			{
				for (int k = 0; k < curSize.z; k++)
				{
					Vector3 size = new Vector3((float)(curSize.x-1) * Margins.x,
						(float)(curSize.y-1) * Margins.y,(float)(curSize.z-1) * Margins.z );
					
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
				
				}
		
			}
		}
		
	}
}
