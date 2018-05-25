using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {
    float lastPressed;
    public GameObject MinimapObject;
	// Use this for initialization
	void Start () {
        lastPressed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        lastPressed -= Time.deltaTime;
        if (Input.GetKey(KeyCode.H) && lastPressed <= 0f)
        {
            lastPressed = 0.2f;
            MinimapObject.SetActive(!MinimapObject.activeSelf);
        }
	}
}
