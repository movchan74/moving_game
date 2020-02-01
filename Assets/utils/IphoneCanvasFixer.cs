using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IphoneCanvasFixer : MonoBehaviour {
	RectTransform rect;
	void Awake()
	{
		
		rect = GetComponent<RectTransform> ();
		ApplySafeArea ();
	}

	void ApplySafeArea()
	{
		Rect area = Screen.safeArea;
		//Debug.Log("Set safe area anchors before: anchorMin: x="+mainScreen.anchorMin.x.ToString("F2")+" y="+ mainScreen.anchorMin.y.ToString("F2"));
		//Debug.Log("Set safe area anchors before: anchorMin: x="+mainScreen.anchorMax.x.ToString("F2")+" y="+ mainScreen.anchorMax.y.ToString("F2"));
		//Debug.Log("Set safe area screen size : Screen.width="+Screen.width+" Screen.height="+Screen.height);

		var anchorMin = area.position;
		var anchorMax = area.position + area.size;
		//Debug.Log("Set safe area size : area.position="+area.position+" area.size="+area.size);
		anchorMin.x /= Screen.width;
		anchorMin.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;
		if (anchorMin.x <= 0f)
			anchorMin.x = 0f;
		if (anchorMin.y <= 0f)
			anchorMin.y = 0f;
		if (anchorMax.x >= 1f)
			anchorMax.x = 1f;
		if (anchorMax.y >= 1f)
			anchorMax.y = 1f;
		rect.anchorMin = anchorMin;
		rect.anchorMax = anchorMax;

		//Debug.Log("Set safe area anchorMin: x="+anchorMin.x.ToString("F2")+" y="+ anchorMin.y.ToString("F2"));
		//Debug.Log("Set safe area anchorMax: x="+anchorMax.x.ToString("F2")+" y="+ anchorMax.y.ToString("F2"));
	}
}
