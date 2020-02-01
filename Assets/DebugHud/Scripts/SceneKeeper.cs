using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeeper : MonoBehaviour 
{
    static public SceneKeeper Inst;
   bool firstLoad = true;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
        
       
    }

    

}
