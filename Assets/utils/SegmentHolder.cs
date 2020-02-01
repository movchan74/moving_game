using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HyperCasual.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class SegmentHolder : MonoBehaviour
{
    List<GameObject> currentPrefabList = new List<GameObject>();
    public float DestroyDist = 20;
    
    public List<GameObject> SegmentPrefabList = new List<GameObject>();
    public GameObject StartSegment;
    
    List<Segment> SegmentList = new List<Segment>();
    public Transform StartTransform;
    Transform currentTransform;
    
    public static SegmentHolder Inst;
    public int MaxSegs = 5;
    
    public GameObject TestSegment;

    private bool UseTestSegment;
   // public Transform FollowTar;

   
    public GameObject MidSegmentPrefab;
    public GameObject EndSegmentPrefab;
    public float MidSegmentLength = 15;
    

    
    private void Awake()
    {
        Inst = this;
        var list = FindObjectsOfType<Segment>().ToList();
        list.DespawnList();
       
    }


    public void Reset()
    {
        if (TestSegment != null)
            UseTestSegment = true;
        
        //MidSegmentLength = GM.Inst.curLevelData.MidSectionLength;
        MidSegmentLength = DataHolder.Data.MidSegmentLength;
    //   MaxSegs = GM.Inst.curLevelData.SegmentCount; //uncomment this if you want to use this method
//        SegmentPrefabList = GM.Inst.curLevelData.SegmentList; //uncomment this if you want to use this method
        currentPrefabList = SegmentPrefabList.Shuffle().ToList();
        currentTransform = StartTransform;
        SegmentList.DespawnList();
        AddSegment(StartSegment);

        for (int i = 0; i < MaxSegs; i++)
        {
            AddMidSegment();
            AddSegment(GetNextSegmentPrefab());
           
        }
        AddSegment(EndSegmentPrefab);
        
    }
    
    private void FixedUpdate()
    {
        
       // UpdateSegments();
//        if (FollowTar.transform.position.z > currentSephardSegment.Exit.transform.position.z)
//        {
//            
//            int index = SegmentList.IndexOf(currentSephardSegment);
//            currentSephardSegment = SegmentList[index + 1];
//            PlayerEnterSegment(currentSephardSegment);
//        }
          
    }
    
    public void AddSegment(GameObject prefab)
    {
        
        Segment segment = Utils.SpawnObject(prefab, transform).GetComponent<Segment>();
        segment.transform.position = currentTransform.position + (segment.transform.position - segment.start.position);
        currentTransform = segment.end;
        SegmentList.Add(segment);
        segment.Reset();
       
            

    }

    public void AddMidSegment()
    {
        Segment segment = Utils.SpawnObject(MidSegmentPrefab, transform).GetComponent<Segment>();
        segment.GetComponent<MidSegment>().SetLength(MidSegmentLength);
        segment.transform.position = currentTransform.position + (segment.transform.position - segment.start.position);
        currentTransform = segment.end;
        SegmentList.Add(segment);
        segment.Reset();
    }
    
//    void UpdateSegments()
//    {
//
//
//
//        if (SegmentList.Count < MaxSegs)
//        {
//            if (TestSegment == null)
//            {
//                //AddSegment(SegmentPrefabList.GetRandom());
//                AddSegment(GetNextSegmentPrefab());
//            }
//               
//            else AddSegment(TestSegment);
//        }
//           
//        
//        
//        Segment oldest = SegmentList[0];
//        if (FollowTar.position.z - oldest.end.transform.position.z > DestroyDist)
//        {
//            //Debug.Log("f");
//            SegmentList.Remove(oldest);
//            Destroy(oldest.gameObject);
//        }
//    }
    
    GameObject GetNextSegmentPrefab()
    {
        if (UseTestSegment)
            return TestSegment;
        
        if (currentPrefabList.Count == 0)
            currentPrefabList = SegmentPrefabList.Shuffle().ToList();  
        
        GameObject prefab = currentPrefabList[Random.Range(0, currentPrefabList.Count)];
        currentPrefabList.Remove(prefab);
        return prefab;

        
       
        
    }

//    private void OnDestroy()
//    {
//        GameStateManager.ResetEvent -= GameStateManagerOnResetEvent;
//    }
}
