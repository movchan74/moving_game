using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;



public class PlacerRoom : ObjectPlacerBase
{
    public Vector3 Size;
    public float WallsThickness = 5;
    public bool showWalls = true;
    public bool showCeil = true;
    public bool showFloor = true;
    
    public override void PlaceObjects()
    {
        var orgRot = transform.rotation;
        transform.rotation = Quaternion.identity;
        
        base.PlaceObjects();

        if (PrefabList.Count == 0)
            return;

        List<GameObject> wallList = new List<GameObject>();
        
        float borderHeight = Size.y;
        float borderWidth = WallsThickness;
        Vector3 size = Size;

        int count = 0;
        for (int i = 0; i < 6; i++)
        {
            GameObject wall = Utils.SpawnObject(GetPrefab(), transform);
            wallList.Add(wall);
        }

        var top = wallList[0].transform;
        var right = wallList[1].transform;
        var bottom = wallList[2].transform;
        var left = wallList[3].transform;
        var floor = wallList[4].transform;
        var ceil = wallList[5].transform;
        
        
        
        top.localPosition = new Vector3(0,0,size.z*0.5f + borderWidth * 0.5f);
        top.localScale = new Vector3(size.x + borderWidth*2,borderHeight, borderWidth);
		bottom.localPosition = new Vector3(0,0,-size.z*0.5f - borderWidth * 0.5f);
        bottom.localScale = top.localScale;
        right.localPosition = new Vector3(size.x * 0.5f + borderWidth * 0.5f, 0);
        right.localScale = new Vector3(borderWidth,borderHeight, size.z + borderWidth*2);
        left.localPosition = new Vector3(-size.x * 0.5f - borderWidth * 0.5f, 0);
        left.localScale = right.localScale;
        floor.localScale = new Vector3(Size.x, WallsThickness, Size.z);
        floor.localPosition = new Vector3(0,-Size.y*0.5f - WallsThickness * 0.5f);
        ceil.localScale = floor.localScale;
        ceil.localPosition = new Vector3(0,Size.y*0.5f + WallsThickness * 0.5f);
        
        Utils.SetVisible(ceil.gameObject,showCeil );
        Utils.SetVisible(floor.gameObject,showFloor);

        for (int i = 0; i < 4; i++)
        {
            Utils.SetVisible(wallList[i].gameObject,showWalls);
        }

        transform.rotation = orgRot;

    }
}
