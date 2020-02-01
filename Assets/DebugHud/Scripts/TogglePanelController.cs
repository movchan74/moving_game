using UnityEngine;
using System.Collections;

namespace TabtaleSuperCasual
{

	public class TogglePanelController : MonoBehaviour 
	{
		public DebugManager debugManager;

		void Awake()
		{
			debugManager = FindObjectOfType <DebugManager> ();
		}

		public void ToggleValueChanged(bool value)
		{
			string param = gameObject.name.Replace("TogglePanel","");
			if (debugManager)
				debugManager.SetParam(param, value ? "True" : "False");
		}
	}
}
