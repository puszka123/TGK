using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerGUI : MonoBehaviour
{
    GUIStyle guiStyle = new GUIStyle();
    public string ObjectName;
    bool _show = false;
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
        //show what the player can pick
        if (_show)
        {
            guiStyle.fontSize = Screen.height / 40;
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontStyle = FontStyle.Bold;
            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            texture.Apply();
            guiStyle.normal = new GUIStyleState { textColor = Color.white, background = texture };
            if (ObjectName != "Skrzynia")
            {
                GUIContent content = new GUIContent("Podnieś " + ObjectName);
                Vector2 size = guiStyle.CalcSize(content);
                GUI.Label(new Rect(Screen.width / 2 - size.x/2, 50, size.x, size.y), content, guiStyle);
            }
            else
            {
                GUIContent content = new GUIContent("Otwórz " + ObjectName);
                Vector2 size = guiStyle.CalcSize(content);
                GUI.Label(new Rect(Screen.width / 2 - -size.x/2, 50, size.x, size.y), content, guiStyle);
            }
        }
    }

    void SetShow(bool show)
    {
        _show = show;
    }
}
