using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelController : MonoBehaviour {
    public GameObject Player;
    public GameObject Rim;
    bool _impossible = false;

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
            gameObject.SendMessage("SetShow", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            gameObject.SendMessage("SetShow", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player && Input.GetKey(KeyCode.E))
        {
            if(Player.GetComponent<PlayerMissionManager>().HasKey)
            {
                OpenJewelCase();
            }
            else
            {
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetAction", "Zamknięte! Gdzieś musi być klucz...");
            }
        }
    }

    void OpenJewelCase()
    {
        if (GameObject.FindGameObjectWithTag("storyobject").GetComponent<StoryObject>().Missions.Contains("find_gold") && !_impossible)
        {
            if (Rim.GetComponent<EnemySight>().CanSee)
            {
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetAction", "Rim cię widział! Nie udało się wykonać misji!");
                _impossible = true;
            }
            else
            {
                GameObject.FindGameObjectWithTag("storyobject").GetComponent<StoryObject>().CompleteMission("rim_find_gold");
                gameObject.SetActive(false);
            }
        }
    }
}
