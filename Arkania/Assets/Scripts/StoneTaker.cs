using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTaker : MonoBehaviour
{
    public GameObject Player;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("storyobject").GetComponent<StoryObject>().Missions.Contains("rim_find_gold"))
            if (other.gameObject == Player && Input.GetKey(KeyCode.E))
            {
                gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetImportant", "Użyj G by rzucić kamień");
                other.SendMessage("TakeStone");
            }
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
}
