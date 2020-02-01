using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : Mover 
{
	Vector3 origSca;
	Vector3 baseSca;
	public Vector3 StartSca, EndSca, ScaRange;
	public AnimationCurve curve;
	public override void Reset()
	{
		origSca = transform.localScale;
		baseSca = origSca + Utils.RandomModVector(ScaRange); 

	}

	public override void UpdatePos(float ratio)
	{
		Vector3 vec1 = baseSca + StartSca;
		Vector3 vec2 = baseSca + EndSca;
		Vector3 vec3 = Vector3.Lerp(vec1, vec2, curve.Evaluate(ratio));
		transform.localScale = vec3;


	}
}
