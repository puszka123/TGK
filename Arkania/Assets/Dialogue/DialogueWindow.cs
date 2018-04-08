using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueWindow : MonoBehaviour {

    private int cursor = 0;

    bool show = false;
    //string[] shown_options;


    FirstPersonController PlayerController;
    DialogueActor actor;
    private int current_option_set = 0;


    // Use this for initialization
    void Start () {
        
        PlayerController = GameObject.FindObjectOfType<FirstPersonController>();
        Debug.Log(PlayerController.name);
        actor = gameObject.GetComponent<DialogueActor>();

        //current_option_set = actor.getOptionSetById("0");

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cursor >= 1) cursor--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cursor <= actor.Dialogue[current_option_set].Count - 2) cursor++;
        }
    }

    void OnGUI()
    {
        if(show)
        {
            //string[] shown_options = option_list[current_option_set];

            GUI.Label(new Rect(10, 10, 300, 20), actor.Questions[current_option_set]);

            List<string> options = actor.Dialogue[current_option_set];
            for (int i = 0; i < options.Count; i++)
            {
                string msg = "";
                if (cursor == i) msg += ">>> ";
                msg += options[i];

                GUI.Label(new Rect(10, 10 + 50 * (i + 1), 300, 20), msg);
            }
        }

    }

    public void OnTalkPrompt()
    {
        if (show == false)
        {
            current_option_set = actor.getOptionSetById("0");
            show = true;
            cursor = 0;
            PlayerController.CanMoveCamera = false;
            PlayerController.CanMove = false;

        }
        else
        {
            Debug.Log(cursor);
            Debug.Log(current_option_set);
            string action = actor.Actions[current_option_set][cursor];

            string[] command = action.Split(' ');



            if (command.Length > 1 && command[0].Equals("set"))
            {
                current_option_set = actor.getOptionSetById(command[1]);
            }
            else
            {
                show = false;
                PlayerController.CanMoveCamera = true;
                PlayerController.CanMove = true;
            }

            
        }



    }

}
