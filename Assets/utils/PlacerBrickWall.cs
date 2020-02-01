using System.Collections;
using System.Collections.Generic;
using HyperCasual.Extensions;
using UnityEngine;



public class PlacerBrickWall : ObjectPlacerBase
{
    public Vector3 Size;

    public Vector3Int BrickCount;
    public Vector3 Offset = new Vector3(0,0.5f, 0);
    public override void PlaceObjects()
    {
        base.PlaceObjects();

        if (PrefabList.Count == 0)
            return;

        if (Size.x <= 0 || Size.y <= 0 || Size.z <= 0 || BrickCount.x <= 0 || BrickCount.y <= 0 || BrickCount.z <= 0)
            return;

        Vector3 brickSize = new Vector3( Size.x / (float) BrickCount.x, Size.y / (float) BrickCount.y, Size.z / (float) BrickCount.z);
        
        for (int i = 0; i < BrickCount.x; i++)
        {
            for (int j = 0; j < BrickCount.y; j++)
            {
                for (int k = 0; k < BrickCount.z; k++)
                {
                    GameObject prefab = GetPrefab();
					
                    
					
                    Vector3 pos = new Vector3(-Size.x * 0.5f + brickSize.x * (float)i,
                                      -Size.y * 0.5f + brickSize.y * (float)j,
                                      -Size.z * 0.5f + brickSize.z * (float)k) + 
                                  new Vector3((Size.x + brickSize.x) * Offset.x, 
                                      (Size.y + brickSize.y) * Offset.y,
                                      (Size.z + brickSize.z) * Offset.z);


                    float altOffset = (float) ((j + k) % 2);
                    pos.x += altOffset * 0.5f*brickSize.x;
                    GameObject thing = Utils.SpawnObject(prefab, transform, false);
                    thing.transform.localPosition = pos;
                    thing.transform.localScale = brickSize;

                    if (i == 0)
                    {
                        if (altOffset == 0)
                        {
                            thing.transform.SetLocalScaleX(brickSize.x * 0.5f);
                            thing.transform.localPosition += brickSize.x * 0.25f * Vector3.right;
                           
                            
                            pos.x = (Size.x + brickSize.x) * Offset.x +
                                    -Size.x * 0.5f + brickSize.x * (float) (BrickCount.x - 1) + brickSize.x * 0.75f;
                            
                            GameObject thing2 = Utils.SpawnObject(prefab, transform, false);
                            
             
                            thing2.transform.localPosition = pos;
                            
                            thing2.transform.localScale = thing.transform.localScale;
                            
                        }
                        
                    }
                    
                }
		
            }
        }
		
    }

  }

