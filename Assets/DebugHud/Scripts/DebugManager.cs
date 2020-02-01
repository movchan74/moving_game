using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour 
{
	//public ScriptablegeneralData defaultgm.generalData, gm.generalData;
	public GameObject headerPrefab, paramFreePrefab, paramTogglePrefab;

	private enum ParamType { Int=0, Float, Bool, String }
	private class Param 
	{
		public string id;
		public ParamType type;
		public int initIValue;
		public float initFValue;
		public bool initBValue;
		public string initSValue;
		public int currentIValue;
		public float currentFValue;
		public bool currentBValue;
		public string currentSValue;
		public event Action OnUpdate = () => {};
		public InputField input;
		public Toggle toggle;

		public Param()
		{
		}

		public void NotifyUpdate()
		{
			OnUpdate();
		}

		public void SetValue(string value)
		{
			switch(type)
			{
			case ParamType.Float:
				currentFValue = float.Parse(value);
				input.text = value;
				break;
			case ParamType.Int:
				currentIValue = int.Parse(value);
				input.text = value;
				break;
			case ParamType.Bool:
				currentBValue = bool.Parse(value);
				toggle.isOn = currentBValue;
				break;
			default:
				currentSValue = value;
				input.text = value;
				break;
			}
		}
		public string GetValue()
		{
			switch(type)
			{
			case ParamType.Float:
				return currentFValue.ToString();
			case ParamType.Int:
				return currentIValue.ToString();
			case ParamType.Bool:
				return currentBValue.ToString();
			default:
				return currentSValue;
			}
		}
		public string GetInitValue()
		{
			switch(type)
			{
			case ParamType.Float:
				return initFValue.ToString();
			case ParamType.Int:
				return initIValue.ToString();
			case ParamType.Bool:
				return initBValue.ToString();
			default:
				return initSValue;
			}
		}
		public void Reset()
		{
			currentFValue = initFValue;
			currentIValue = initIValue;
			currentBValue = initBValue;
			currentSValue = initSValue;
			if (type == ParamType.Bool)
				toggle.isOn = currentBValue;
			else
				input.text = GetValue();
		}
		public override string ToString()
		{
			return (id + " [" + type + "] = " + GetValue());
		}
	}
		
	private Dictionary<string,Param> parameters = new Dictionary<string, Param>();

	//public GameObject paramPanelPrefab = null;
	//public GameObject headerPanelPrefab = null;


	DataHolder data;
	//public Scriptablegm.generalData currentData;// = new Scriptablegm.generalData();

	void Awake()
	{
		//Debug.Log ("Debug Manager Awake");

		InitDebugPanel();
	}
    public static bool GeneralDataHasResetOnce = false;
	public void ResetGeneralData()
	{
       // if (SceneManager.)

        if (!GeneralDataHasResetOnce)
        {
            data = FindObjectOfType<DataHolder>();
            data.generalData.CopyFrom(data.defaultGeneralData);
            GeneralDataHasResetOnce = true;
        }


		
	}

	public void InitDebugPanel () 
	{

		int count=0;
		data = FindObjectOfType<DataHolder>();
		FieldInfo[] fields = data.generalData.GetType().GetFields();
		foreach(FieldInfo field in fields)
		{
			Param param = new Param();
			param.id = field.Name;
			if (field.FieldType == typeof(float))
			{
				param.type = ParamType.Float;
				param.initFValue = (float)field.GetValue(data.generalData);
				param.currentFValue = param.initFValue;
			}
			else if (field.FieldType ==  typeof(int))
			{
				param.type = ParamType.Int;
				param.initIValue = (int)field.GetValue(data.generalData);
				param.currentIValue = param.initIValue;
			}
			else if (field.FieldType == typeof(bool))
			{
				param.type = ParamType.Bool;
				param.initBValue = (bool)field.GetValue(data.generalData);
				param.currentBValue = param.initBValue;
			}
			else //if (field.FieldType == typeof(string))
			{
				param.type = ParamType.String;
				param.initSValue = (string)field.GetValue(data.generalData);
				param.currentSValue = param.initSValue;
				break;
			}
			parameters.Add(field.Name, param);
			//Debug.Log("Added param: " + param.ToString());

			string paramText = param.id;
			string paramHint = "";
			foreach (object atr in field.GetCustomAttributes(false))
			{
				if (atr.GetType() == typeof(HeaderAttribute))
				{
					GameObject headerPanel = Instantiate(headerPrefab, transform.position, Quaternion.identity) as GameObject;
					headerPanel.transform.SetParent(transform, false);
					headerPanel.transform.Find("Text").GetComponent<Text>().text = ((HeaderAttribute)atr).header;
				}
				else if (atr.GetType() == typeof(DebugTextAttribute))
				{
					paramText = ((DebugTextAttribute)atr).text;
				}
				else if (atr.GetType() == typeof(DebugHintAttribute))
				{
					paramHint = ((DebugHintAttribute)atr).text;
				}
			}

			GameObject paramPanel;
			if (param.type == ParamType.Bool)
			{
				paramPanel = Instantiate(paramTogglePrefab, transform.position, Quaternion.identity) as GameObject;
				paramPanel.name = param.id + "TogglePanel";
				paramPanel.transform.SetParent(transform, false);
				paramPanel.transform.Find("Text").GetComponent<Text>().text = paramText + ":";
				param.toggle = paramPanel.transform.Find("Toggle").GetComponent<Toggle>();
				param.toggle.isOn = param.currentBValue;
			}
			else
			{
				paramPanel = Instantiate(paramFreePrefab, transform.position, Quaternion.identity) as GameObject;
				paramPanel.name = param.id + "FreeParamPanel";
				paramPanel.transform.SetParent(transform, false);
				paramPanel.transform.Find("Text").GetComponent<Text>().text = paramText + ":";
				param.input = paramPanel.transform.Find("InputField").GetComponent<InputField>();
				param.input.text = param.GetValue();
			}
			if (paramHint != "")
			{
				paramPanel.transform.Find("HintButton").Find("Text").GetComponent<Text>().text = paramHint;
				paramPanel.transform.Find("ShowHintButton").gameObject.SetActive(true);
			}
			count++;
		}
		//Debug.Log("debug fields: " + count.ToString());
	}

	private void UpdateCurrentData(string name)
	{
		switch(parameters[name].type)
		{
		case ParamType.Float:
				data.generalData.SetField(name, parameters[name].currentFValue);
			break;
		case ParamType.Int:
				data.generalData.SetField(name, parameters[name].currentIValue);
			break;
		case ParamType.Bool:
				data.generalData.SetField(name, parameters[name].currentBValue);
			break;
		case ParamType.String:
				data.generalData.SetField(name, parameters[name].currentSValue);
			break;
		}
	}
	public void SetParam(string name, string value)
	{
		if (!parameters.ContainsKey(name))
			return;
		parameters[name].SetValue(value);
		UpdateCurrentData(name);
		parameters[name].NotifyUpdate();
	}
	public void IncreaseParam(string param) {}
	public void DecreaseParam(string param) {}
	public void ToggleParam(string param) {}

	public void Save(string setup)
	{
		foreach(Param param in parameters.Values)
		{
			Debug.Log("Saving param: " + param.ToString());
			TTPlayerPrefs.SetString("Setup"+"_"+setup+"_"+param.id, param.GetValue());
		}
	}
	public void Reset()
	{
		foreach(Param param in parameters.Values)
		{
			param.Reset();
			UpdateCurrentData(param.id);
			param.NotifyUpdate();
		}
	}

	public void Load(string setup)
	{
		if (setup == "0")
		{
			Reset();
			return;
		}
		foreach(Param param in parameters.Values)
		{
			Debug.Log("Loading param: " + param.ToString() + " getting: " + TTPlayerPrefs.GetString("Setup"+"_"+setup+"_"+param.id, param.GetInitValue()));
			param.SetValue(TTPlayerPrefs.GetString("Setup"+"_"+setup+"_"+param.id, param.GetInitValue()));
			UpdateCurrentData(param.id);
			param.NotifyUpdate();

		}
	}

	public string V3toS(Vector3 v)
	{
		return "(" + v.x.ToString("0.00") + "," + v.y.ToString("0.00") + "," + v.z.ToString("0.00") + ")";
	}

	//public delegate void WatchFunction();
	public void RequestWatchParam(string name, Action watcher)
	{
		if (parameters.ContainsKey(name))
			parameters[name].OnUpdate += watcher;
	}
	public void RequestUnwatchParam(string name, Action watcher)
	{
		if (parameters.ContainsKey(name))
			parameters[name].OnUpdate -= watcher;
	}

}

