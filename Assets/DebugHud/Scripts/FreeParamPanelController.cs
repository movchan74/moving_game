using UnityEngine;
using System.Collections;

namespace TabtaleSuperCasual
{

	public class FreeParamPanelController : MonoBehaviour 
	{
		public DebugManager debugManager;

		void Awake()
		{
			debugManager = FindObjectOfType <DebugManager> ();
		}

		public void ValueChanged(string value)
		{
			string param = gameObject.name.Replace("FreeParamPanel","");
			debugManager.SetParam(param, value);
		}
	}

}
