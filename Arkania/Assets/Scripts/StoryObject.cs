using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoryObject : MonoBehaviour {

    List<string> missions;
    List<bool> missionStatuses;
    bool[] _activateRimConditions = new bool[2];
    public GameObject RimFirstLocation;
    public GameObject RimSecondLocation;
    public GameObject Rim;
    public GameObject Indicator;
    public GameObject MinimapCamera;
    GameObject[] others;


    // Use this for initialization
    void Start() {
        others = GameObject.FindGameObjectsWithTag("actor");
        missions = new List<string>();
        missionStatuses = new List<bool>();
        foreach(GameObject go in others)
        {
             go.SetActive(false);
        }
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

        //if both are true then activate Rim
        if (mission == "find_gold")
        {
            _activateRimConditions[0] = true;

        }
        if (mission == "find_children") _activateRimConditions[1] = true;
        if (mission == "find_moner") ActiveOthers();
        if (mission == "follow_rim") ActivateRimPart2();
        if (mission == "find_gold") ChangeBorenNextId();

        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission) return;
        }
        missions.Add(mission);
        missionStatuses.Add(false);
        
        //start rim part
        if (_activateRimConditions[0] && _activateRimConditions[1]) StartCoroutine(ActivateRim());
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

    IEnumerator ActivateRim()
    {
        _activateRimConditions[0] = _activateRimConditions[1] = false;
        yield return new WaitForSeconds(2);
        Rim.transform.SetPositionAndRotation(RimFirstLocation.transform.position, transform.rotation);
        Rim.SendMessage("RimScream");
        SendMessage("SetAction", "Sprawdź co się stało!");
        Indicator.SendMessage("SetIndicatedLocation", RimFirstLocation);
        //MinimapCamera.GetComponent<Minimap>().MinimapObject.SetActive(true);
    }

    void ActivateRimPart2()
    {
        Rim.transform.SetPositionAndRotation(RimSecondLocation.transform.position, transform.rotation);
        SendMessage("SetAction", "Podążaj za Rimem");
        Indicator.SendMessage("SetIndicatedLocation", RimSecondLocation);
    }

    void ActiveOthers()
    {
        foreach (GameObject go in others)
        {
            if (go != gameObject) go.SetActive(true);
        }
    }

    void ChangeBorenNextId()
    {
        GameObject boren = others.Select(e => e).Where(e => e.GetComponent<DialogueActor>().actorName == "Boren").ToArray()[0];
        //if you have already talked to Boren -> change his nextid to 1lb 
        if (!String.Equals(boren.GetComponent<DialogueWindow>().NextId, "0"))
        {
            Debug.Log(boren.GetComponent<DialogueActor>().actorName + " " + boren.GetComponent<DialogueWindow>().NextId);
            boren.SendMessage("ChangeNextId", "1LB");
        }
    }

    public void TryToChangeBorenNextId()
    {
        foreach (var mission in missions)
        {
            if(String.Equals(mission, "find_gold"))
            {
                GameObject boren = others.Select(e => e).Where(e => e.GetComponent<DialogueActor>().actorName == "Boren").ToArray()[0];
                boren.SendMessage("ChangeNextId", "1LB");
            }
        }
    }
}
