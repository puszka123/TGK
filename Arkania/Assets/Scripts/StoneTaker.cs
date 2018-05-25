using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTaker : MonoBehaviour {
    public GameObject Player;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == Player && Input.GetKey(KeyCode.E))
        {
            gameObject.SetActive(false);
            other.SendMessage("TakeStone");
        }
    }
}
