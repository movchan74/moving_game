using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
	public float blinkInterval = 0.05f;
	public float blinkDuration = 0.6f;

	public float startBlinkTime;
	bool blinking;
	//bool imageOn;


	SpriteRenderer sr;
	Timer timer;

	void Awake()
	{
		//timer = new Timer (0);
		sr = GetComponent<SpriteRenderer> ();
	}

	public void StartBlink(float _blinkInterval = -1, float _blinkDuration = -1)
	{
		if (_blinkInterval > 0)
			blinkInterval = _blinkInterval;

		if (_blinkDuration > 0)
			blinkDuration = _blinkDuration;

		startBlinkTime = Time.time;
		blinking = true;
		//timer = new Timer (blinkInterval);
		sr.enabled = false;
	}

	void Update()
	{
		if (blinking)
		{
			if (timer.Ratio () >= 1)
			{
				if (sr.enabled)
					sr.enabled = false;
				else
					sr.enabled = true;

				timer.Reset (blinkInterval);
			}

			if (Time.time - startBlinkTime >= blinkDuration)
			{
				blinking = false;
				sr.enabled = true;
			}

		}
	}

}
