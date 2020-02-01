using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HyperCasual.Extensions
{
	public static class TransformChildrenExtensions
	{
		public static List<Transform> GetChildrenList(this Transform transform)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				
				list.Add(transform.GetChild(i));
			}

			return list;
		}
	
		
	}
	
	
	
	
}
