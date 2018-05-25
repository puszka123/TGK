using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissionManager : MonoBehaviour {

    bool _hasStone = false;
    public GameObject Stone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //rim find gold - check if the player can throw a stone
        if (_hasStone)
        {
            if (Input.GetKey(KeyCode.G))
            {
                ThrowStone();
            }
        }
	}

    public void TakeStone()
    {
        Debug.Log("You've taken a stone");
        _hasStone = true;
    }

    //rim find gold mission:
    void ThrowStone()
    {
        _hasStone = false;
        Rigidbody clone = Instantiate(Stone.GetComponent<Rigidbody>(), transform.position, transform.rotation) as Rigidbody;
        clone.AddForce(transform.TransformDirection(Vector3.forward * 1000));
    }
}
