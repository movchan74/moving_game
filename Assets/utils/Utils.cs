using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using HyperCasual.Extensions;

[Serializable]
public class FloatRange  
{
	public float min=0, max=0;

	public FloatRange(float minValue=0, float maxValue=0)
	{
		min = minValue;
		max = maxValue;
	}

	public static FloatRange CrossLerp(FloatRange range1, FloatRange range2, float value)
	{
		FloatRange rangeMin = new FloatRange (range1.min, range2.min);
		FloatRange rangeMax = new FloatRange (range1.max, range2.max);
		FloatRange rangeLerped = new FloatRange (rangeMin.Lerp (value), rangeMax.Lerp (value));
		return rangeLerped;
	}

	public float Clamp(float value)
	{
		if (value <= min)
			return min;
		if (value >= max)
			return max;
		return value;
	}

	public float Lerp(float value)
	{
		return (min + (max - min) * Mathf.Clamp01 (value));
	}

	public float GetRatio(float value)
	{
		return Mathf.Clamp01((value - min) / (max - min));
	}

    public Vector3 ClampVelocity(Vector3 vector)
    {
        if (vector.magnitude > max)
        {
            vector = vector.normalized * max;
        }
        else if (vector.magnitude < min)
        {
            vector = vector.normalized * min;
        }

        return vector;
             
    }


	public float Delta { get { return max-min; } }

	public float Random { get  { return UnityEngine.Random.Range(min,max); } }

	public static FloatRange operator+(FloatRange lhs, FloatRange rhs)
	{
		return new FloatRange (lhs.min + rhs.min, lhs.max + rhs.max);
	}


}

[Serializable]
public class IntRange  
{
	public int min=0, max=0;

	public IntRange(int minValue=0, int maxValue=0)
	{
		min = minValue;
		max = maxValue;
	}

	public int Clamp(int value)
	{
		if (value <= min)
			return min;
		if (value >= max)
			return max;
		return value;
	}

	public int Delta { get { return max-min; } }

	public int Random { get  { return UnityEngine.Random.Range(min,max+1); } }
}


public static class Utils
{

	public static float RandomMod(float mod)
	{
		return (UnityEngine.Random.Range (-mod, mod));
	}

	public static GameObject SpawnObject(GameObject prefab, Transform parent = null, bool _pool = false)
	{
        GameObject GO;

        if (!_pool)
        {
           GO = MonoBehaviour.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        }
        else
        {
            GO = SimplePool.Spawn(prefab, Vector3.zero, Quaternion.identity);
        }
	
		GO.transform.SetParent (parent);
		GO.transform.localPosition = Vector3.zero;
		GO.transform.localRotation = Quaternion.identity;
		return GO;
	}



	public static Vector3 GetPositionOnPointList(List<Vector3> pointList, float factor)
	{
		factor = Mathf.Clamp01 (factor);
		int point1 = Mathf.FloorToInt ((float)(pointList.Count - 1) * factor);
		int point2 =  Mathf.CeilToInt ((float)(pointList.Count - 1) * factor);
		float pointRatio = (float)(pointList.Count - 1) * factor - (float)point1;
		Vector3 pos = Vector3.Lerp (pointList [point1], pointList [point2], pointRatio);
		return pos;
	}

	public static Vector3 GetClosestPosition(List<Vector3> pointList, Vector3 point)
	{
		Vector3 pos = pointList.OrderByDescending (x => (x - point).magnitude).Last ();
		return pos;

	}

	public static List<int> GetRandomIntSetFromIntGroup(int setSize, int groupSize)
	{
		List<int> group = new List<int> ();

		for (int i = 0; i < groupSize; i++)
		{
			group.Add (i);
		}

		List<int> set = new List<int> ();

		for (int i = 0; i < setSize; i++)
		{
			int r = UnityEngine.Random.Range (0, group.Count);
			set.Add (group [r]);
			group.Remove (group [r]);
		}

		return set;

	}

    public static int GetRandomIntWithExclusion(List<int> list, int exclude)
    {
        var list1 = new List<int>();
        list1.AddRange(list);
        list1.Remove(exclude);
        return list1[UnityEngine.Random.Range(0, list1.Count)];
    }

    public static Vector3 RandomModVector(Vector3 vector)
    {

        return (new Vector3(Utils.RandomMod(vector.x), Utils.RandomMod(vector.y), Utils.RandomMod(vector.z)));
    }

    public static void DespawnList<T>(this List<T> ListToClear, bool removeGO = true) where T : Component
    {
        if (ListToClear == null){
            Debug.LogError("ListData is Null");
        }


       
            //foreach (T obj in ListToClear)
            //{
            //    MonoBehaviour.DestroyImmediate(obj.gameObject);
            //}


            foreach (T obj in ListToClear)
            {
	            if (obj != null)
	            {
		            if (removeGO)
		            {
		           
			            MonoBehaviour.DestroyImmediate(obj.gameObject);
		            }
		            else
		            {
			            MonoBehaviour.DestroyImmediate(obj);
		            }
	            }
	           
	            
             
            }
       

        ListToClear.Clear();
    }

    public static List<int> CreateIntList(int count)
    {
        List<int> list = new List<int>();

        for (int i = 0; i < count; i++)
        {
            list.Add(i);
        }

        return list;
    }

    public static void SetVisible(GameObject _object, bool on)
    {
	    var list = _object.GetComponentsInChildren<MeshRenderer>().ToList();
	    MeshRenderer myMesh = _object.GetComponent<MeshRenderer>();
	    if (myMesh != null) list.Add(myMesh);
	    
	    
	    foreach (MeshRenderer mr in list)
	    {
		    mr.enabled = on;

	    }
    }

    public static void SetMatOffline(GameObject _object, Material _mat)
    {
	    var tempMat = new Material(_mat);
	    
	    var list = _object.GetComponentsInChildren<MeshRenderer>().ToList();
	    MeshRenderer myMesh = _object.GetComponent<MeshRenderer>();
	    if (myMesh != null) list.Add(myMesh);

	    foreach (MeshRenderer mr in list)
	    {
		    var j = mr.materials.Length;
		    
		    var l = new List<Material>();

		    for (int i = 0; i < j; i++)
		    {
			    l.Add(tempMat);
		    }

		    mr.materials = l.ToArray();
		    mr.material = tempMat;
	    }
    }
    
    public static void SetMat( GameObject _object, Material _mat)
    {
	    
	    var tempMat = new Material(_mat);
	    
	    var list = _object.GetComponentsInChildren<MeshRenderer>().ToList();
	    MeshRenderer myMesh = _object.GetComponent<MeshRenderer>();
	    if (myMesh != null) list.Add(myMesh);

	    foreach (MeshRenderer mr in list)
	    {
		    var j = mr.sharedMaterials.Length;
		    
		    var l = new List<Material>();

		    for (int i = 0; i < j; i++)
		    {
			    l.Add(tempMat);
		    }

		    mr.sharedMaterials = l.ToArray();
		    mr.sharedMaterial = tempMat;
	    }
    }


    public static void SetMatLerp(GameObject _object, Material _mat1, Material _mat2, float r)
    {
	    
	    var list = _object.GetComponentsInChildren<MeshRenderer>().ToList();
	    MeshRenderer myMesh = _object.GetComponent<MeshRenderer>();
	    if (myMesh != null) list.Add(myMesh);

	    if (r == 0)
	    {
		    SetMat(_object, _mat1);
		    return;
	    }
	    
	    if (r == 1)
	    {
		    SetMat(_object, _mat2);
		    return;
	    } 
	    
	    var tempMat = new Material(_mat1); 
	    tempMat.Lerp( _mat1, _mat2, r);
	    
	    foreach (MeshRenderer mr in list)
	    {
		    var j = mr.sharedMaterials.Length;
		    
		    var l = new List<Material>();

		    for (int i = 0; i < j; i++)
		    {
			    l.Add(tempMat);
		    }

		    mr.sharedMaterials = l.ToArray();
		    mr.sharedMaterial = tempMat;
	    }

	    
    }
    
    public static void SetMatAlpha(GameObject _object, float a)
    {
	    var list = _object.GetComponentsInChildren<MeshRenderer>().ToList();
	    MeshRenderer myMesh = _object.GetComponent<MeshRenderer>();
	    if (myMesh != null) list.Add(myMesh);
	    
	    
	    foreach (MeshRenderer mr in list)
	    {
		    foreach (var mat in mr.materials)
		    {
			    //var mat =_object.GetComponentInChildren<MeshRenderer>().material;
			    var c = mat.color;
			    c.a = a;
			    mat.color = c;
		    }
		    
	    }
	    
	  
    }
    

    public static float GetCamDistSizeMod(Camera cam, float d1, float d2)
    {
	    
	    
	    var a1 = cam.transform.forward * d1;
	    var a2 = cam.transform.forward * d1 + Vector3.up;
	    var b1 = cam.transform.forward * d2;
		var b2 = cam.transform.forward * d2 + Vector3.up;

		var a1v = cam.WorldToViewportPoint(a1);
		var a2v = cam.WorldToViewportPoint(a2);
		var b1v = cam.WorldToViewportPoint(b1);
		var b2v = cam.WorldToViewportPoint(b2);

		var r1 = a2v.y - a1v.y;
		var r2 = b2v.y - b1v.y;

		var mod = (r1 / r2);

		return mod;
	    
	    
    }


}
