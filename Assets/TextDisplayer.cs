using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ObjectTextData
{
   // public string StartText = "I need to put some things in a box";
    //public string EndText = "All the things I need are in the box";
    public List<string> TextList = new List<string>();
    public List<string> AltTextList = new List<string>();
}

public class TextDisplayer : MonoBehaviour
{
//    public ObjectTextData StartText;
//    public ObjectTextData EndText;
//    
//    public List<ObjectTextData> TextList = new List<ObjectTextData>();

    public Text TextObject;

    public ObjectTextData curTextData;
   

    public float AltTextDur = 1;
    private bool showAltText = false;

    public static TextDisplayer Inst;

    public int curTextInd = 0;
    
    private void Awake()
    {
        Inst = this;
        LevelStateHandler.NewStateSetEvent += NewStateSetEvent;
        InputListener.TouchScreen += InputListenerOnTouchScreen;
    }

    public bool TextIsActive;
    
    private void InputListenerOnTouchScreen(Vector3 obj)
    {
        
        if (LevelStateHandler.state == LevelStateHandler.State.Beginning)
        {
            curTextInd++;
            if (curTextInd > curTextData.TextList.Count - 1)
            {
                LevelStateHandler.Inst.TextCompleted();
            }

            return;
        }
        
        if (showAltText)
        {
            curTextInd++;
            if (curTextInd > curTextData.AltTextList.Count - 1)
                HideAltText();
        }
        
        
        
        curTextInd = (curTextInd + 1);
        
        if (curTextInd > curTextData.TextList.Count - 1)
        {
            if (LevelStateHandler.state == LevelStateHandler.State.Ending)
            {
                GM.Inst.EndGame();
                return;
            }
            
            TextIsActive = false;
            curTextInd = curTextData.TextList.Count - 1;
        }
        else
        {
            TextIsActive = true;
        }
            
    }

    private void OnDestroy()
    {
        LevelStateHandler.NewStateSetEvent -= NewStateSetEvent;
    }

    private void FixedUpdate()
    {
        if (TextIsActive)
        { if (!showAltText)
                TextObject.text = curTextData.TextList[curTextInd];
            else
            {
                TextObject.text = curTextData.AltTextList[curTextInd];
            }
        }

        if (ObjectHandler.Inst.state == ObjectHandler.State.Holding)
            TextObject.text = "";
    }

    private void NewStateSetEvent(LevelStateHandler.State obj)
    {
        switch (obj)
        {
            case LevelStateHandler.State.Beginning:
                
                StartNewTextStream(DataHolder.Params.StartText);
                break;
            case LevelStateHandler.State.SearchForObject:
                
                StartNewTextStream(DataHolder.Params.TextList[LevelStateHandler.CurrentObjInt]);
                
                break;
            case LevelStateHandler.State.Ending:
                
                StartNewTextStream(DataHolder.Params.EndText);
                break;
        }
    }

    void StartNewTextStream(ObjectTextData textData)
    {
        TextIsActive = true;
        curTextInd = 0;
        curTextData = textData;
    }
    
    public void ShowAltText()
    {
        TextIsActive = true;
       // CancelInvoke();
        showAltText = true;
        curTextInd = 0;
        //Invoke("HideAltText", AltTextDur);
    }

    public void HideAltText()
    {
        TextIsActive = true;
        curTextInd = 0;
        showAltText = false;
    }
}
