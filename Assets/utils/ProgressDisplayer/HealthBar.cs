using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
	public RectTransform fill;

	public float Health;

	private void Update()
	{
		Health = Mathf.Clamp01(Health);
		var sca = fill.localScale;
		sca.x = Health;
		fill.localScale = sca;
	}
}
