using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SpherePlacerData
{
    public float Rad;
    public int Count = 1000;
    public float SizeMod = 10;
    public bool RotToCenter = true;

}

        


    public class SpherePlacer:ObjectPlacerBase
    {
        public List<SpherePlacerData> SphereDataList = new List<SpherePlacerData>();

        public override void PlaceObjects()
        {
            base.PlaceObjects();

            if (PrefabList.Count == 0)
                return;

            foreach (var d in SphereDataList)
            {
                CreateSphere(d);
            }
        }

        void CreateSphere(SpherePlacerData data)
        {
            
            List<Vector3> upts = new List<Vector3>();
            float inc  = Mathf.PI * (3 - Mathf.Sqrt(5));
            float off  = 2 / (float)data.Count;
            float x;
            float y;
            float z;
            float r;
            float phi;
   
            for (var k = 0; k < data.Count; k++)
            {
                y = k * off - 1 + (off /2);
                r = Mathf.Sqrt(1 - y * y);
                phi = k * inc;
                x = Mathf.Cos(phi) * r;
                z = Mathf.Sin(phi) * r;
       
                upts.Add(new Vector3(x, y, z)*data.Rad);
            }

            foreach (var v in upts)
            {
                GameObject prefab = GetPrefab();
                var thing = Utils.SpawnObject(prefab, transform);
                thing.transform.localPosition = v;
                var t = thing.GetComponent<RandomPlacer>();
                if (t != null)
                    t.Randomize();

                thing.transform.localScale *= data.SizeMod;
                if (data.RotToCenter)
                    thing.transform.forward = thing.transform.position - transform.position;
            }
            
           
            
        }

        
    }

