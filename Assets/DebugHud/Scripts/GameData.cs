using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "General/GameData", menuName = "General/GameData", order = 1)]

public class GameData : ScriptableObject
{
    public ObjectTextData StartText;
    public ObjectTextData EndText;
    
    public List<ObjectTextData> TextList = new List<ObjectTextData>();
    public float ReleaseVel = 2;
}