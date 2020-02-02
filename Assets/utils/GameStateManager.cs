using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameStateManager 
    : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject NextLevelButton;
    public static GameStateManager Inst;
    public static event Action ResetEvent = () => { };
    public static event Action NextButtonClicked = () => { };
    public static event Action PrevButtonClicked = () => { };

    public bool ReloadSceneOnReset = true;

    private void Awake()
    {
        //InputListener.TouchScreen += InputListenerOnTouchScreen;
    }

//    private void InputListenerOnTouchScreen(Vector3 obj)
//    {
//        if (PlayButtonDown)
//            PlayButtonDown = false;
//    }


    public enum State
    {
        Play,
        Pause
    }

    public State state;

    void Start()
    {
        Inst = this;
        Reset();
    }

    public void Reset()
    {
        PlayButton.SetActive(false);
        NextLevelButton.SetActive(false);
        Time.timeScale = DataHolder.Data.TimeScale;
        ResetEvent();
        state = State.Play;
    }

    public void EndGame()
    {
        PlayButton.SetActive(true);
        Time.timeScale = 0;
        state = State.Pause;
    }
    
    public void EndLevel()
    {
        NextLevelButton.SetActive(true);
        //Time.timeScale = 0;
        state = State.Pause;
    }

   // public bool PlayButtonDown = false;
    
    public void PlayButtonPressed()
    {
        //PlayButtonDown = true;
        
        if (!ReloadSceneOnReset)
            Reset();
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Invoke("Reset", 0.01f);
        }
    }

    
    
    public void BackButtonPressed()
    {
        //NextButtonClicked();
    }
    public void NextButtonPressed()
    {
        NextButtonClicked();
        PlayButtonPressed();
    }
    
    public void PrevButtonPressed()
    {
        PrevButtonClicked();
        PlayButtonPressed();
    }
    

    
}