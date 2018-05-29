using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{

    bool _canTalk = false;
    bool _noAction = true;
    bool _showMap = false;
    string _npcName = "";
    GUIStyle guiStyle = new GUIStyle();
    string _actionMessage = "";
    float _elapsedTime = 60f;
    bool _showTime = false;
    bool important = false;
    string importantMessage = "";
 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (_canTalk && _noAction)
        {
            guiStyle.fontSize = Screen.height / 40;
            //guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            texture.Apply();
            string text = "Rozmawiaj z " + _npcName;
            GUIContent content = new GUIContent(text);
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, 10, size.x, size.y), content, guiStyle);
        }
        if (!_noAction)
        {
            guiStyle.fontSize = Screen.height / 40;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            texture.Apply();
            GUIContent content = new GUIContent(_actionMessage);
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, 10, size.x, size.y), content, guiStyle);
            StartCoroutine(DisableActionMessage());
        }
        if (_showTime)
        {
            guiStyle.fontSize = Screen.height / 40;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            texture.Apply();
            GUIContent content = new GUIContent("Znajdź złoto!");
            GUIContent content1 = new GUIContent(_elapsedTime.ToString("0.00"));
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            Vector2 size1 = guiStyle.CalcSize(content1);
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, guiStyle.fontSize + 80, size.x, size.y), content, guiStyle);
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, 2 * guiStyle.fontSize + 80, size1.x, size1.y), content1, guiStyle);
        }

        if(important)
        {
            guiStyle.fontSize = Screen.height / 40;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            texture.Apply();
            GUIContent content = new GUIContent(importantMessage);
            Vector2 size = guiStyle.CalcSize(content);
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            GUI.Label(new Rect(Screen.width / 2 - size.x/2, Screen.height / 2 - size.y / 2, size.x, size.y), content, guiStyle);
            StartCoroutine(DisableActionMessage());
            StartCoroutine(DisableActionMessage());
        }
    }

    IEnumerator DisableActionMessage()
    {
        yield return new WaitForSeconds(5);
        _noAction = true;
        important = false;
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
        if (message.StartsWith("Rim cię widział! Nie udało się wykonać misji!")) _showTime = false;
        StopAllCoroutines();
        _noAction = false;
        _actionMessage = message;
    }

    public void ShowTimeElapsed(float time)
    {
        _showTime = true;
        _elapsedTime = time;
    }

    public void DisableTime()
    {
        _showTime = false;
    }

    public void SetImportant(string message)
    {
        StopAllCoroutines();
        important = true;
        importantMessage = message;
    }
}
