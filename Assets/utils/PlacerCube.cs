using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class PlacerCube : ObjectPlacerBase 
{
	public Vector3Int Size;
	public Vector3 Margins = new Vector2(3,3);
	public Vector3 MarginsMod;
	public Vector3 Offset = new Vector3(0,0.5f,0);
	public Vector3 ObjScale = Vector3.one;
	
	public Vector3 AlternatingOffset = new Vector3();
	public Vector3 ConstantOffset = new Vector3();
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
					count++;
					if (SelectMethod == PrefabSelectionMethod.Sequential)
					{
						prefab = PrefabList[count % PrefabList.Count];
						
					}
					else
					{
						prefab = PrefabList.GetRandom();
					}
					
					Vector3 pos = new Vector3(-size.x * 0.5f + Margins.x * i, -size.y * 0.5f + Margins.y * j, -size.z * 0.5f  + Margins.z * k) + 
					              new Vector3((size.x + prefab.transform.localScale.x * ObjScale.x) * Offset.x, 
						              (size.y + prefab.transform.localScale.y* ObjScale.y) * Offset.y,
						              (size.z + prefab.transform.localScale.z* ObjScale.z) * Offset.z);

					if (k % 2 == 1)
						pos += AlternatingOffset;

					pos += (float) count * ConstantOffset;
					
					GameObject thing = Utils.SpawnObject(prefab, transform, false);
					thing.transform.localPosition = pos;
					thing.transform.localScale = new Vector3(thing.transform.localScale.x * ObjScale.x,thing.transform.localScale.y * ObjScale.y,
						thing.transform.localScale.z * ObjScale.z );
					thing.transform.position += new Vector3(Utils.RandomMod(MarginsMod.x),Utils.RandomMod(MarginsMod.y),
						Utils.RandomMod(MarginsMod.z) );

					var rp = thing.GetComponent<RandomPlacer>();
					if (rp!=null)
						rp.Randomize();

				}
		
			}
		}
		
	}
}
