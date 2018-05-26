using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Rim;
    bool _impossible = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetAction", "Muszę jakoś odwrócić uwagę Rima...");
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
        if (GameObject.FindGameObjectWithTag("storyobject").GetComponent<StoryObject>().Missions.Contains("rim_find_gold"))
            if (other.gameObject == Player && Input.GetKey(KeyCode.E) && !_impossible)
            {
                if (Rim.GetComponent<EnemySight>().CanSee)
                {
                    GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetAction", "Rim cię widział! Nie udało się wykonać misji!");
                    _impossible = true;
                }
                else
                {
                    Player.SendMessage("TakeKey");
                    gameObject.SetActive(false);
                }
            }
    }
}
