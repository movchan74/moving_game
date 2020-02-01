using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreHolder : MonoBehaviour {

	public Text scoreText;
	public Text bestScoreText;
	public int score;
	public static ScoreHolder scoreHolder;
    //public float scoreChangeDuration = 0.5f;
    //public int TargetScore = 0;
    //public int LastScore = 0;
   // ScoreHolder ActualScoreHolder;
    //Timer timer;
   // public int scoresSet = 0;
	void Awake()
	{
        //scoreHolder = this;
        //timer = gameObject.AddComponent<Timer> ();
        if (scoreHolder == null)
            scoreHolder = this;

    }


    private void OnEnable()
    {

    }

    private void Update()
    {
      // ActualScoreHolder = ScoreHolder.scoreHolder;
    }

    public void SetScore (int newScore)
	{

       //scoresSet++;
        //TargetScore = newScore;
        score = newScore;
       // Debug.Log(scoreText);
        scoreText.text = score.ToString();
        UpdateBestScore(score);
    }

	

	public void UpdateBestScore (int newScore)
	{
		if (PlayerPrefs.GetInt ("Best", 0) < newScore)
		{
			PlayerPrefs.SetInt ("Best", newScore);

		}

		bestScoreText.text = "Best: " + PlayerPrefs.GetInt ("Best", 0).ToString ();

	}



	public void ResetHighScore()
	{
		PlayerPrefs.SetInt ("Best", 0);
	}

	
}
