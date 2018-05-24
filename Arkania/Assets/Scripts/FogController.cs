using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    public GameObject PlayerObject;

	// Use this for initialization
	void Start () {
        RenderSettings.fog = false;
        RenderSettings.fogDensity = 0.03f;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == PlayerObject)
        {
            RenderSettings.fog = !RenderSettings.fog;
            Light light = GameObject.FindGameObjectWithTag("sun").GetComponent<Light>();
            if (!RenderSettings.fog)
            {
                light.intensity = 1f;
            }
        }
    }
}
