using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;

using UnityEngine;

public class PlacerCircular : ObjectPlacerBase {

	public float StartRad = 10;
	public float StartAngle = 0;
	public float AngleChangePerRing = 0.1f;
	public float CoverPercent = 1;
	
	public int Circles = 3;
	public float Interval = 1.25f;
	public float CircleInterval = 1.25f;
	public bool RotateDots = false;
	public float RotateDotAmount = 0;
	public Vector3 PrefabSize;
	public float RandomRad;
	
	
	public override void PlaceObjects()
	{
		base.PlaceObjects();

		if (PrefabList.Count == 0)
			return;
		
		GameObject prefab;
		int count = 0;
		for (int i = 0; i < Circles; i++)
		{

			if (SelectMethod == PrefabSelectionMethod.Sequential)
			{
				prefab = PrefabList[count % PrefabList.Count];
				count++;
			}
			else
			{
				prefab = PrefabList.GetRandom();
			}

			float rad = StartRad + CircleInterval * (float) i;
			float circum = Mathf.PI * 2 * rad;
			int dotAmount = Mathf.FloorToInt((circum * CoverPercent) / Interval);
			float angInterval = (CoverPercent * 360f) / (float) dotAmount;
			for (int j = 0; j < dotAmount; j++)
			{
				GameObject dot = Utils.SpawnObject(prefab, transform, false);
				dot.SetActive(true);
				dot.transform.localRotation = Quaternion.identity;
				dot.transform.localPosition =
					transform.localPosition + Quaternion.Euler(0, (StartAngle + AngleChangePerRing *(float)i)* 360 + transform.eulerAngles.y
					                                                          + (float) j * angInterval, 0) *
					Vector3.forward * rad;

				if (RotateDots)
				{
					dot.transform.forward = (dot.transform.localPosition - transform.localPosition);
					dot.transform.Rotate(Vector3.up, RotateDotAmount);
				}
				if (PrefabSize.magnitude > 0)
				dot.transform.localScale = PrefabSize;

				var randRad = Random.onUnitSphere;
				randRad.y = 0;
				randRad *= Random.value * RandomRad;
				dot.transform.localPosition += randRad;

			}
		}

	}
}
