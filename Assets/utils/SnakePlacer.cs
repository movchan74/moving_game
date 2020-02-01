using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;

public class SnakePlacer : ObjectPlacerBase
{
    public float SectionSize = 1;
    public float SectionInterval = 1;
    public Rigidbody Head;
    public Rigidbody Tail;
    public Vector3 ConnectionVector = Vector3.forward;

    public int SectionCount;

    public float SectionMaxAngle = 60;


    public float HeadInterval, TailInterval;
    public List<GameObject> sectionList;


    public Material SectionMat;

    public Material TailMat;
    public Material HeadMat;

    private Quaternion origRot;
    
    public override void PlaceObjects()
    {
       // origRot = transform.rotation;
        //transform.rotation = Quaternion.identity;
        
        
        foreach (var g in sectionList)
            DestroyImmediate(g.gameObject);

        sectionList.Clear();

        var list = transform.GetChildrenList();
        foreach (var v in list)
            DestroyImmediate(v.gameObject);

        Vector3 startPoint = Vector3.zero;
        if (Head != null)
            startPoint = Head.transform.localPosition + HeadInterval * ConnectionVector.normalized;

        int sectionCount = SectionCount;
        if (Tail != null)
            sectionCount += 1;


        for (int i = 0; i < sectionCount; i++)
        {
            GameObject section;
            if (i < SectionCount)
            {
                var prefab = GetPrefab();
                section = Utils.SpawnObject(prefab, transform);
                section.SetActive(true);
                section.transform.localPosition =
                    startPoint + ConnectionVector.normalized * SectionInterval * (float) i;
                section.transform.localScale *= SectionSize;
                sectionList.Add(section);

                
            }
            else
            {
                section = Tail.gameObject;
                section.transform.localPosition =
                    sectionList[i - 1].transform.localPosition +
                    TailInterval * ConnectionVector.normalized;
            }


            var rb = section.GetComponent<Rigidbody>();

            if (rb == null)
                rb = section.AddComponent<Rigidbody>();


            var j = section.GetComponent<ConfigurableJoint>();
            var s = section.GetComponent<SnakeSection>();
            if (i == 0 && Head != null)
            {
                j.connectedBody = Head;
                if (s != null)
                {
                    s.prevNode = Head;
                    s.maxDist = HeadInterval + s.margin;
                }
                   
                

            }
            else
            {
                j.connectedBody = sectionList[i - 1].GetComponent<Rigidbody>();
                if (s != null)
                {
                    s.prevNode = sectionList[i - 1].GetComponent<Rigidbody>();
                    s.maxDist = SectionInterval + s.margin;
                }
            }
            
            
            
           
        }


        if (SectionMat != null)
        {
            foreach (var g in sectionList)
            {
                Utils.SetMat(g.gameObject, SectionMat);
            
            }
        }

        if (HeadMat != null)
        {
            Utils.SetMat(Head.gameObject, HeadMat);
        }

        if (TailMat != null)
        {
            Utils.SetMat(Tail.gameObject, TailMat);
        }
       
        
        jointTransformList.Clear();
        
        if (Head != null)
            jointTransformList.Add(Head.transform);

        for (int i = 0; i < sectionList.Count; i++)
        {
            jointTransformList.Add(sectionList[i].transform);
        }

        if (Tail != null)
        {
            jointTransformList.Add(Tail.transform);
        }
       
    }

    public List<Transform> jointTransformList = new List<Transform>();
    
    public void SetTransformOnPath(Transform targetTransform, float r)
    {
        

        if (jointTransformList.Count < 2)    
            return;
        
        if (r <= 0)
        {
            
            targetTransform.position = jointTransformList[0].position;
            var forward = jointTransformList[1].position - jointTransformList[0].position;
            //forward = jointTransformList[0].forward;
            targetTransform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            //targetTransform.rotation = jointTransformList[0].rotation;
            return;
        }
        
        if (r >= 1)
        {
            targetTransform.position = jointTransformList[jointTransformList.Count - 1].position;
            var forward = jointTransformList[jointTransformList.Count - 1].position - jointTransformList[jointTransformList.Count - 2].position;

            targetTransform.rotation = Quaternion.LookRotation(forward, Vector3.up);
           // targetTransform.forward = jointTransformList[jointTransformList.Count - 1].forward;
            return;
        }

        float num = r * (float) (jointTransformList.Count -1);
        int int0 = Mathf.FloorToInt(num);
        int int1 = Mathf.CeilToInt(num);
        
        
            
        
        float ratio = num - (float) int0;

        var j0 = jointTransformList[int0];
        var j1 = jointTransformList[int1];
        
        
        var pos = Vector3.Lerp(j0.position, j1.position,
            ratio);

        
        Vector3 for1 = j1.position - j0.position;
        Vector3 for2 = for1;
        
        if (int1 < jointTransformList.Count - 1)
            for2 = jointTransformList[int1 + 1].position - j1.position;

        var rot1 = Quaternion.LookRotation(for1, Vector3.up);
        var rot2 = Quaternion.LookRotation(for2, Vector3.up);
       // rot1 = j0.rotation;
       // rot2 = j1.rotation;
        
        
        var rot = Quaternion.Lerp(rot1, rot2,
            ratio);

        targetTransform.position = pos;
        targetTransform.rotation = rot;

    }
}