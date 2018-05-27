using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwonstairsController : MonoBehaviour {
    public GameObject Player;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            GameObject.FindGameObjectWithTag("storyobject").SendMessage("EndTheGame");
        }
    }

}
