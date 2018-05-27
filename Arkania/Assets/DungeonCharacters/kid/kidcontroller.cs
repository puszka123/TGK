using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidcontroller : MonoBehaviour {
    public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            GameObject.FindGameObjectWithTag("storyobject").SendMessage("KidFound");
            gameObject.SetActive(false);
        }
    }
}
