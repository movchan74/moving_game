using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
public class DebugHintAttribute : Attribute
{
	public readonly string text;
	public DebugHintAttribute(string value)
	{
		text = value;
	}
}

