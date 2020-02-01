using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerController : MonoBehaviour
{
   private CharacterController cc;
   private void Awake()
   {
       cc = GetComponent<CharacterController>();

   }

   public float Speed;
   public float LookSpeed;
   public float haxis;
   private void FixedUpdate()
   {
      if (Input.GetKey(KeyCode.W))
      {
          cc.Move(Vector3.forward * Speed * Time.fixedDeltaTime);

      }

      haxis = Input.GetAxis("Horizontal");
   }
}
