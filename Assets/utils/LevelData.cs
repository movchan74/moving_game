using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "General/LevelData", menuName = "General/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<GameObject> SegmentList = new List<GameObject>();
    public int SegmentCount = 6;
    //public Shoe.ControlMode controlMode = Shoe.ControlMode.Manual;


}