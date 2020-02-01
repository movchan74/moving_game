using UnityEngine;
using System.Collections;
using System.Reflection;

[CreateAssetMenu (fileName = "General/GeneralParams", menuName = "PathFinderCube/General Data", order = 1)]
public class ScriptableGeneralData : ScriptableObject
{


	[Header("General")] 
	
	
	
	
	public float TimeScale = 1;
    public float JoystickSize = 1000;

    
    public bool ShowJoystick = false;
    public float MidSegmentLength = 1;


    public void CopyFrom (ScriptableGeneralData otherData)
	{
		foreach (FieldInfo field in GetType().GetFields())
			field.SetValue (this, field.GetValue (otherData));
	}

	public void SetField (string name, object value)
	{
		foreach (FieldInfo field in GetType().GetFields())
			if (field.Name == name)
				field.SetValue (this, value);
	}
}
