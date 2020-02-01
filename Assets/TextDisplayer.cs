using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ObjectTextData
{
    public string StartText = "I need to put some things in a box";
    public string EndText = "All the things I need are in the box";
    
   
}



public class TextDisplayer : MonoBehaviour
{
    public ObjectTextData GameStartEndText;
    
    public List<ObjectTextData> TextList = new List<ObjectTextData>();

    public Text TextObject;

    public string curText;
    public string altText;

    public float AltTextDur = 1;
    private bool showAltText = false;

    public static TextDisplayer Inst;
    
    private void Awake()
    {
        Inst = this;
        LevelStateHandler.NewStateSetEvent += NewStateSetEvent;
    }

    private void OnDestroy()
    {
        LevelStateHandler.NewStateSetEvent -= NewStateSetEvent;
    }

    private void FixedUpdate()
    {
        if (!showAltText)
            TextObject.text = curText;
        else
        {
            TextObject.text = altText;
        }
    }

    private void NewStateSetEvent(LevelStateHandler.State obj)
    {
        switch (obj)
        {
            case LevelStateHandler.State.Beginning:
                curText = GameStartEndText.StartText;
                break;
            case LevelStateHandler.State.SearchForObject:
                curText = TextList[LevelStateHandler.CurrentObjInt].StartText;
                break;
            case LevelStateHandler.State.CompletedObject:
                curText =  TextList[LevelStateHandler.CurrentObjInt].EndText;
                break;
            case LevelStateHandler.State.Ending:
                curText = GameStartEndText.EndText;
                break;
        }
    }

    public void ShowAltText()
    {
        CancelInvoke();
        showAltText = true;
        Invoke("HideAltText", AltTextDur);
    }

    public void HideAltText()
    {
        showAltText = false;
    }
}
