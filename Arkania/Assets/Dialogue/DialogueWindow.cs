using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DialogueWindow : MonoBehaviour {

    private int cursor = 0;
    string _nextId = "0";

    bool show = false;
    //string[] shown_options;


    FirstPersonController PlayerController;
    ThirdPersonCamera Camera;
    DialogueActor actor;
    public StoryObject story;
    
    private int current_option_set = 0;

    private GUIStyle guiStyle = new GUIStyle();


    // Use this for initialization
    void Start () {
        
        PlayerController = GameObject.FindObjectOfType<FirstPersonController>();
        Camera = GameObject.FindObjectOfType<ThirdPersonCamera>();
        actor = gameObject.GetComponent<DialogueActor>();


    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (cursor >= 1) cursor--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (cursor <= actor.Dialogue[current_option_set].Count - 2) cursor++;
        }
    }

    void OnGUI()
    {
        guiStyle.fontSize = 10;
        guiStyle.normal.textColor = Color.white;
        if (show)
        {

            Texture2D targetTexture = new Texture2D(350, 200);
            //GetComponent<Renderer>().material.mainTexture = targetTexture;

            Color[] fillColorArray = targetTexture.GetPixels();

            for (int i = 0; i < fillColorArray.Length; ++i)
            {
                Color color = new Color(0,0,0);
                color.a = 0.7f;
                fillColorArray[i] = color;
            }

            targetTexture.SetPixels(fillColorArray);

            targetTexture.Apply();



            GUI.DrawTexture(new Rect(Screen.width / 2 - 150, Screen.height * 3 / 5, 350, 200), targetTexture);

            GUI.Label(new Rect(Screen.width / 2 - 150 + 10, Screen.height*3/5 + 10, 300, 20), actor.Questions[current_option_set], guiStyle);


            List<string> options = actor.Dialogue[current_option_set];
            for (int i = 0; i < options.Count; i++)
            {
                string msg = "";
                if (cursor == i) msg += ">>> ";
                msg += options[i];

                GUI.Label(new Rect(Screen.width/2 - 150 + 10, Screen.height * 3 / 5 + 50 + 15 * (i + 1), 300, 20), msg, guiStyle);
            }
        }

    }

    public void OnTalkPrompt()
    {
        if (show == false)
        {
            current_option_set = actor.getOptionSetById(_nextId);
            show = true;
            cursor = 0;
            Camera.SetCanMove(false);
            PlayerController.CanMove = false;

        }
        else
        {
            string action = actor.Actions[current_option_set][cursor];

            string[] command = action.Split(' ');

            List<string> missions = actor.Missions[current_option_set];

            foreach(string mission in missions) {
                if(mission != null && mission != "")
                {
                    story.AddMission(mission);
                }
            }

            List<string> missionCompletes = actor.MissionCompletes[current_option_set];


            foreach (string missionComplete in missionCompletes)
            {
                if (missionComplete != null && missionComplete != "")
                {
                    story.CompleteMission(missionComplete);
                }
            }

            List<string> nextIdCandidates = actor.NextIds[current_option_set];
            Debug.Log(current_option_set + " " + nextIdCandidates.Count);
            foreach(string nextid in nextIdCandidates)
                if (!String.IsNullOrEmpty(nextid))
                {
                    _nextId = nextid;
                    break;
                }

            if (command.Length > 1 && command[0].Equals("set"))
            {
                current_option_set = actor.getOptionSetById(command[1]);
                cursor = 0;
            }
            else
            {
                show = false;
                Camera.SetCanMove(true);
                PlayerController.CanMove = true;
            }

            
        }



    }

}
