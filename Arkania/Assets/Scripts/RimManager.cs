using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RimManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RimScream()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
