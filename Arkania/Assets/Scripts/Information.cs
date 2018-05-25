using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour {

    bool _canTalk = false;
    bool _noAction = true;
    bool _showMap = false;
    string _npcName = "";
    GUIStyle guiStyle = new GUIStyle();
    string _actionMessage = "";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (_canTalk && _noAction)
        {
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 100), "Rozmawiaj z " + _npcName, guiStyle);
        }
        if(!_noAction)
        {
            guiStyle.fontSize = 20;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 100), _actionMessage, guiStyle);
            StartCoroutine(DisableActionMessage());
        }
    }

    IEnumerator DisableActionMessage()
    {
        yield return new WaitForSeconds(5);
        _noAction = true;
    }

    public void ActivateTalk(string npcName)
    {
        _canTalk = true;
        _npcName = npcName;
    }

    public void NoTalk()
    {
        _canTalk = false;
    }

    public void SetAction(string message)
    {
        _noAction = false;
        _actionMessage = message;
    }
}
