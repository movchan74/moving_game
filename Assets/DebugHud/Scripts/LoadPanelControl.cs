using UnityEngine;
using System.Collections;

namespace TabtaleSuperCasual
{

	public class LoadPanelControl : MonoBehaviour 
	{
		public DebugManager debugManager;

		void Awake()
		{
			debugManager = FindObjectOfType <DebugManager> ();
		}

		public void Load(string setup)
		{
			debugManager.Load(setup);
		}
	}
}