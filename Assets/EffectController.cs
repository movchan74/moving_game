using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public enum EffectState
    {
        Normal,
        Bottle,
        Mirror,
        Photo
    }


    public static EffectController Inst;
    
    
    public LayerMask NormalMask;
   public LayerMask MirrorMask;
   public LayerMask BottleMask;
   public LayerMask PhotoMask;
   public LayerMask Nothing;

   private void Awake()
   {
       Inst = this;
   }

   public void SetMask(EffectState _state)
   {
       switch (_state)
       {
           case (EffectState.Normal):
               SetNormal();
               break;
           case (EffectState.Bottle):
               SetBottle();
               break;
           case (EffectState.Mirror):
               SetMirror();
               break;
           case (EffectState.Photo):
               SetPhoto();
               break;
           
       }
   }
   
   
   public void SetNormal()
   {
       GM.Inst.cam.cullingMask = NormalMask;
   }
   
   public void SetMirror()
   {
       GM.Inst.cam.cullingMask = MirrorMask;
   }
   
   public void SetBottle()
   {
       GM.Inst.cam.cullingMask = BottleMask;
   }
   
   public void SetPhoto()
   {
       GM.Inst.cam.cullingMask = PhotoMask;
   }

   public void SetBlack()
   {
       GM.Inst.cam.cullingMask = Nothing;
   }
}
