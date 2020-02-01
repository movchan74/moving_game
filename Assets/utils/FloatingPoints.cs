using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingPoints : MonoBehaviour {

	public Vector3 speed;
	public Vector3 offset = new Vector3 (0, 2);
	public float lifeTime;

	public int points;
	float alpha;
	Text text;
	float showPointTime = 0;
	//float alphaDecayRate = 0;
	public Color color;
	void Awake()
	{
		text = GetComponentInChildren<Text> ();
	}

	public void Init()
	{
		SetAlpha (0);
		points = 0;
		text.color = color;
		//alphaDecayRate = Time.fixedDeltaTime / lifeTime;
	}

	public void ShowPoints( int _points)
	{
		showPointTime = Time.time;
		//transform.position = pos + offset;
		SetPoints (_points);
		SetAlpha (1);
	}

	void Update()
	{
		if (alpha > 0)
		{
			transform.position += speed*Time.deltaTime;
			SetAlpha (1 -Mathf.Clamp01 ( (Time.time - showPointTime )/ lifeTime));
		}
		else
		{

			SetAlpha (0);
			SetPoints (0);
			SimplePool.Despawn (gameObject);
		}

	}


	void SetAlpha(float a)
	{
		alpha = a;
		Color c = color;
		text.color = new Color (c.r, c.b, c.g, a);
	}

	void SetPoints(int p)
	{
		points = p;
		text.text = "+" + points.ToString ();
	}

}
