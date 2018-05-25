using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour {
   
    public GameObject Player;
    GameObject _indicatedLocation;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        if (_indicatedLocation != null)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            RotateIndicator();
            if(Vector3.Distance(transform.position, _indicatedLocation.transform.position) < 20f)
            {
                _indicatedLocation = null;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    void RotateIndicator()
    {
        var lookPos = _indicatedLocation.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    public void SetIndicatedLocation(GameObject location)
    {
        _indicatedLocation = location;
    }
}
