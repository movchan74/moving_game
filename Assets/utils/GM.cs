using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GM : MonoBehaviour
{
   // public LevelData curLevelData;

   
    public static GM Inst;

    
    public Camera cam;

   // public RigidbodyFirstPersonController controller;

 public bool IsTestScene = false;
 
 
    private void Awake()
    {
        Inst = this;
        GameStateManager.ResetEvent += GameStateManagerOnResetEvent;
        GameStateManager.NextButtonClicked += GameStateManagerOnNextButtonClicked;
        GameStateManager.PrevButtonClicked += GameStateManagerOnPrevButtonClicked;
        //LevelStateHandler.NewStateEvent += InstOnNewStateEvent;
        
    }

    private void OnDestroy()
    {
        GameStateManager.ResetEvent -= GameStateManagerOnResetEvent;
        GameStateManager.NextButtonClicked -= GameStateManagerOnNextButtonClicked;
        GameStateManager.PrevButtonClicked -= GameStateManagerOnPrevButtonClicked;
        // LevelStateHandler.NewStateEvent -= InstOnNewStateEvent;
    }
    
//    private void InstOnNewStateEvent(LevelStateHandler.State obj)
//    {
//        if (obj == LevelStateHandler.State.Play)
//        {
//            //GameplayManager.Inst.StartGameplayPhase();
//            //SideLimiter.Inst.SetGameplayScreenLimits();
//        }
//        else
//        {
//            //SideLimiter.Inst.DisableLimits();
//        }
//    }

    
    private void GameStateManagerOnPrevButtonClicked()
    {
        GoBackLevel();
    }

    private void GameStateManagerOnNextButtonClicked()
    {
        AdvanceLevel();
    }

    private void GameStateManagerOnResetEvent()
    {
       Reset();
    }

    void Reset()
    {
        var inputListener = FindObjectOfType<InputListener>();
        inputListener.camera = cam;
        

    }

   

    void AdvanceLevel()
    {
        LevelCounter.Inst.ChangeLevel(1);
    }

    void GoBackLevel()
    {
        LevelCounter.Inst.ChangeLevel(-1);
    }
    
    public void LevelCompleted()
    {
        GameStateManager.Inst.EndLevel();
    }


    public void EndGame()
    {
        EffectController.Inst.SetBlack();
        GameStateManager.Inst.EndGame();
    }
}
