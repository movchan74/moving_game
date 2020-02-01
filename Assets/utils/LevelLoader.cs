using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public List<GameObject> LevelPrefabList = new List<GameObject>();
    public GameObject curLevel;
    public int curLevelInd;
    public static LevelLoader Inst;

    private void Awake()
    {
        Inst = this;
    }

    public GameObject LoadNextLevel()
    {
        curLevelInd += 1;

        curLevelInd = curLevelInd % LevelPrefabList.Count;
        LoadLevel(curLevelInd);
        return curLevel;
    }

    public  GameObject LoadFirstLevel()
    {
        curLevelInd = 0;
        LoadLevel(curLevelInd);
        return curLevel;
    }

    
    void LoadLevel(int ind)
    {
        //curLevelInd = ind;
        if (curLevel != null)
            Destroy(curLevel.gameObject);

        curLevel = Utils.SpawnObject(LevelPrefabList[ind], transform);
    }
}
