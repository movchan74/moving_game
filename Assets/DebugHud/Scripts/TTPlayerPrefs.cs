using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TTPlayerPrefs 
{
	public static void SetInt(string name, int value)
	{
		PlayerPrefs.SetInt(name, value);
		PlayerPrefs.Save();
	}
	public static void SetFloat(string name, float value)
	{
		PlayerPrefs.SetFloat(name, value);
		PlayerPrefs.Save();
	}
	public static void SetBool(string name, bool value)
	{
		PlayerPrefs.SetInt(name, value ? 1 : 0);
		PlayerPrefs.Save();
	}
	public static void SetString(string name, string value)
	{
		PlayerPrefs.SetString(name, value);
		PlayerPrefs.Save();
	}
	public static int GetInt(string name, int defaultValue=0)
	{
		return PlayerPrefs.GetInt(name, defaultValue);
	}
	public static float GetFloat(string name, float defaultValue=0f)
	{
		return PlayerPrefs.GetFloat(name, defaultValue);
	}
	public static bool GetBool(string name, bool defaultValue=false)
	{
		return (PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) != 0);
	}
	public static string GetString(string name, string defaultValue="")
	{
		return PlayerPrefs.GetString(name, defaultValue);
	}

	public static void DeleteAll(){
		PlayerPrefs.DeleteAll();
	}
}

