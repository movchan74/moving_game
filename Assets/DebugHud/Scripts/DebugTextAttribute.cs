using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
public class DebugTextAttribute : Attribute
{
	public readonly string text;
	public DebugTextAttribute(string value)
	{
		text = value;
	}
}

