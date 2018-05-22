using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryObject : MonoBehaviour {

    List<string> missions;
    List<bool> missionStatuses;


    

    // Use this for initialization
    void Start() {
        missions = new List<string>();
        missionStatuses = new List<bool>();
    }

    // Update is called once per frame
    void OnGUI() {
        for (int i = 0; i < missions.Count; i++)
        {
            GUI.Label(new Rect(10, 10 + 30 * i, 300, 300), missions[i] + " " + missionStatuses[i]);

        }
    }

    public void AddMission(string mission) {
        Debug.Log("adding mission " + mission);
        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission) return;
        }
        missions.Add(mission);
        missionStatuses.Add(false);
    }

    public List<String> Missions
    {
        get { return missions; }
        set { missions = value; }
    }

    public List<bool> MissionStatuses
    {
        get { return missionStatuses; }
        set { missionStatuses = value; }
    }

    public void CompleteMission(string missionComplete)
    {
        Debug.Log("completing mission " + missionComplete);

        for (int i = 0; i < missions.Count; i++)
        {
            if(missionComplete == (string)missions[i])
            {
                missionStatuses[i] = true;
                break;
            }
        }
    }
}
