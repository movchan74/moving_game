using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
	public Color BackCol, ChangeCol, DecreaseCol, FrontCol;
	public Color TextGoodColor, TextBadColor, TextNeutralColor;
	
	public HealthBar ChangeBar, FrontBar;
	public Image BackImage;
	private Image ChangeImage, FrontImage;

	public float LerpFactor = 10;

	public Text text;

	public float shownPercent;
	private float tarPercent;
	
	//public float Tar

	public float MinDif = 0.01f;

	public float Score;

	public static ProgressDisplay Inst;
	
	private void Awake()
	{
		Inst = this;
		ChangeImage = ChangeBar.GetComponentInChildren<Image>();
		FrontImage = FrontBar.GetComponentInChildren<Image>();
	}

	private void FixedUpdate()
	{
		FrontImage.color = FrontCol;
		BackImage.color = BackCol;
		
		
		shownPercent = Mathf.Lerp(shownPercent, tarPercent, LerpFactor * Time.fixedDeltaTime);
	
		
		
		float scoreText = Mathf.Round(shownPercent * 1000)/10;
		Score = scoreText;
		text.text = scoreText.ToString();
		
		if (Mathf.Abs(shownPercent - tarPercent) < MinDif)
		{
			SetStatic();
			
		}
		else
		{
			if (tarPercent < shownPercent)
			{
				SetDecreasing();
			}
			else
			{

				SetIncreasing();
			}
		}
		
		if (shownPercent == 1 && tarPercent == 1)
			ReportLevelCompleted();
		
	}

	void ReportLevelCompleted()
	{
		//GM.Inst.EndLevel();
	}
	
	void SetStatic()
	{
		shownPercent = tarPercent;
		FrontBar.Health = tarPercent;
		ChangeBar.Health = tarPercent;
		ChangeImage.color = FrontCol;
		text.color = TextNeutralColor;
	}
	
	void SetDecreasing()
	{
		FrontBar.Health = tarPercent;
		ChangeImage.color = DecreaseCol;
		ChangeBar.Health = shownPercent;
		text.color = TextBadColor;
	}

	void SetIncreasing()
	{
		FrontBar.Health = shownPercent;
		ChangeImage.color = ChangeCol;
		ChangeBar.Health = tarPercent;
		text.color = TextGoodColor;
	}

	public void SetScore(float _newScore)
	{
		tarPercent = _newScore;
	}
}
