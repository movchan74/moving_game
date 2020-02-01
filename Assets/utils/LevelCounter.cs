using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
   public static LevelCounter Inst;

   private void Awake()
   {
      if (Inst == null)
      {
         Inst = this;
         CurLevelInd = 0;
      }
      
   }

   public int CurLevelInd = 0;

   public void ChangeLevel(int change = 1)
   {
      //Debug.Log(CurLevelInd);
      //CurLevelInd = (CurLevelInd + change) % DataHolder.Params.LevelPrefab.Count;
      
   }
}
