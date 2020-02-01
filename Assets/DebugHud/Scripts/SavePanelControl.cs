using UnityEngine;
using System.Collections;

namespace TabtaleSuperCasual
{

	public class SavePanelControl : MonoBehaviour 
	{
		public DebugManager debugManager;

		void Awake()
		{
			debugManager = FindObjectOfType <DebugManager> ();
		}

		public void Save(string setup)
		{
			debugManager.Save(setup);
		}
	}
}