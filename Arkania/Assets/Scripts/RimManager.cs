using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RimManager : MonoBehaviour {
    GameObject _stone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_stone != null)
        {
            
            Look();
        }
	}

    public void RimScream()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    public void LookAtStone(Rigidbody stone)
    {
        _stone = stone.gameObject;
        Debug.Log(_stone.transform.position);
    }

    void Look()
    {
        var lookPos = _stone.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
}
