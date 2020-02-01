using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour 
{
	public ScriptableGeneralData generalData;
	public ScriptableGeneralData defaultGeneralData;
    public GameData gameData;

   // public Transform DotHolder;
   // public ScriptableGeneralData tempData;
	public static ScriptableGeneralData Data;
    public static GameData Params;

    //public List<Transform> CamGroup = new List<Transform>();

    public bool MakeGeneralDefault = true;
    
	void Awake()
	{
        if (Data == null)
		Data = generalData;

        Params = gameData;

        
	}

	private void Update()
	{
		if (MakeGeneralDefault)
		{
			defaultGeneralData.CopyFrom(generalData);
			MakeGeneralDefault = false;
		}
	}
}
