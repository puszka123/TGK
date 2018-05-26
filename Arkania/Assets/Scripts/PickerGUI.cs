using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerGUI : MonoBehaviour {
    GUIStyle guiStyle = new GUIStyle();
    public string ObjectName;
    bool _show = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        //show what the player can pick
        if (_show)
        {
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            if(ObjectName != "Skrzynia") GUI.Label(new Rect(Screen.width / 2 - 50, 50, 100, 100), "Podnieś " + ObjectName, guiStyle);
            else GUI.Label(new Rect(Screen.width / 2 - 50, 50, 100, 100), "Otwórz " + ObjectName, guiStyle);
        }
    }

    void SetShow(bool show)
    {
        _show = show;
    }
}
