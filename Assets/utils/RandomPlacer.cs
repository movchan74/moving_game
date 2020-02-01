using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RandomPlacer : MonoBehaviour
{
   // public FloatRange SizeRange;
    public float PosRange;
    public Vector3 RandomVector;
    public Vector3 RandomRotate;
    
    private void Awake()
    {
        Randomize();
    }

    public void Randomize()
    {
        transform.position += Utils.RandomModVector(RandomVector);
        transform.position += Random.onUnitSphere * Random.value * PosRange;
        //transform.localScale = Vector3.one * SizeRange.Random;
        transform.Rotate(Utils.RandomModVector(RandomRotate));
    }
}
