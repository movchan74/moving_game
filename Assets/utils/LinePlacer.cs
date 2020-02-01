using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

public class LinePlacer : ObjectPlacerBase {

	//public PathCreator PathBase;
	//public int ObjectsOnPathCount;
	public Vector3 MarginsMod;
	public FloatRange PlaceRange = new FloatRange(0,1);
	
	public List<Vector3> OffSetList = new List<Vector3>();
	public Transform startPoint, endPoint;
	public float spaceBetweenObj = 5;

	public float RotOffsetAng = 0;
	public float RotOffsetRad = 10;
	
	
	public override void PlaceObjects()
	{
		base.PlaceObjects();

		float d = Vector3.Distance(startPoint.position, endPoint.position);
		float dr = d * PlaceRange.Delta;
		
		int ObjectsOnPathCount = Mathf.FloorToInt( dr / spaceBetweenObj);
		
		
		float interval = (float) PlaceRange.Delta / (float) ObjectsOnPathCount;

		int count = 0;

		float curAng = 0;
		for (int i = 0; i < ObjectsOnPathCount; i++)
		{
			
			
			
			GameObject prefab;
					
			if (SelectMethod == PrefabSelectionMethod.Sequential)
			{
				prefab = PrefabList[count % PrefabList.Count];
				
			}
			else
			{
				prefab = PrefabList.GetRandom();
			}
			
			count++;
			
			Vector3 pos = Vector3.Lerp(startPoint.position,endPoint.position, PlaceRange.min + (float) i * interval);
			//Vector3 rot =  PathBase.GetPointAlongCurve(PlaceRange.min +(float) i * interval,true);
		
			GameObject thing = Utils.SpawnObject(prefab, transform, false);
			thing.transform.position = pos;
			
			
		
			//thing.transform.rotation = Quaternion.Euler(rot);

			Vector3 offset = Vector3.zero;

			var v = Quaternion.LookRotation( endPoint.position - startPoint.position, Vector3.up);
			v.eulerAngles += new Vector3(0,0,curAng);
			offset += v * Vector3.up * RotOffsetRad;
			curAng += RotOffsetAng;
			
			if (OffSetList.Count > 0)
			{
				offset += OffSetList[count % OffSetList.Count];
			}
			
			offset += new Vector3(Utils.RandomMod(MarginsMod.x),Utils.RandomMod(MarginsMod.y),
				          Utils.RandomMod(MarginsMod.z) );

			
			
			
			thing.transform.position += thing.transform.rotation * offset;

		}
	}
}
