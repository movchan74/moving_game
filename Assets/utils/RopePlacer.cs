using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePlacer : ObjectPlacerBase
{
  
   public Rigidbody StartBody, EndBody;
   public int LinkCount;
   public Vector3 LinkScale;
   public float LinkSpacing = 0.1f;

   public override void PlaceObjects()
   {
      base.PlaceObjects();
      
      if (PrefabList.Count == 0)
         return;

      var vec = EndBody.transform.position - StartBody.transform.position;
      var linkHeight = (vec.magnitude - LinkSpacing * ((float) LinkCount - 1)) / (float) LinkCount;
      LinkScale.y = linkHeight;

      Rigidbody prevRB = StartBody;
      
      for (int i = 0; i < LinkCount; i++)
      {
         GameObject prefab = GetPrefab();
         var link = Utils.SpawnObject(prefab,transform);
         link.transform.position = StartBody.transform.position + vec.normalized * ( (linkHeight + LinkSpacing) *(float)i + linkHeight * 0.5f)  ;
         link.transform.up = vec.normalized;
         link.transform.localScale = LinkScale;
         var j = link.GetComponent<ConfigurableJoint>();
         j.connectedBody = prevRB;
         j.anchor = new Vector3(0,-0.5f, 0);
         prevRB = link.GetComponent<Rigidbody>();
         
         


      }

      var endJ = EndBody.GetComponent<ConfigurableJoint>();
      if (endJ != null)
         endJ.connectedBody = prevRB;

   }
}
