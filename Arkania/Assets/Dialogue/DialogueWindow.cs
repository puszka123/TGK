using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWindow : MonoBehaviour {

    private int cursor = 0;
    private string[] intros = new string[50];
    private string intro1 = "Hello, this is your quest";
    private string intro2 = "Hey, you chose option a";
    private string[] options = { "Option a", "Option b", "Option c" };
    private string[] other_options = { "Option a1", "Option a2" };
    private int current_option_set = 0;
    private string[][] option_list = new string[50][];
    private int option_list_length = 2;
    bool show = false;
    //string[] shown_options;


    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController PlayerController;


    // Use this for initialization
    void Start () {
        intros[0] = intro1;
        intros[1] = intro2;
        option_list[0] = options;
        option_list[1] = other_options;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cursor >= 1) cursor--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (cursor <= options.Length - 2) cursor++;
        }
    }

    void OnGUI()
    {
        if(show)
        {
            string[] shown_options = option_list[current_option_set];

            GUI.Label(new Rect(10, 10, 300, 20), intros[current_option_set]);
            for (int i = 0; i < shown_options.Length; i++)
            {
                string msg = "";
                if (cursor == i) msg += ">>> ";
                msg += shown_options[i];

                GUI.Label(new Rect(10, 10 + 50 * (i+1), 300, 20), msg);

            }
        }

    }

    public void OnTalkPrompt()
    {
        if (current_option_set == 0 && cursor == 0)
        {
            current_option_set = 1;
        }
        else
        {
            ToggleShow();
        }
    }

    public void ToggleShow()
    {
        if (show == false)
        {
            show = true;
            cursor = 0;
            current_option_set = 0;
        }
        else show = false;

        if (show)
        {
            PlayerController.CanMoveCamera = false;
            PlayerController.CanMove = false;
        }
        else
        {
            PlayerController.CanMoveCamera = true;
            PlayerController.CanMove = true;
        }
    }
}
