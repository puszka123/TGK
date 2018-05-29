using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Text;

public class StoryObject : MonoBehaviour
{

    List<string> missions;
    List<bool> missionStatuses;
    bool[] _activateRimConditions = new bool[2];
    public GameObject RimFirstLocation;
    public GameObject RimSecondLocation;
    public GameObject Rim;
    public GameObject Indicator;
    public GameObject MinimapCamera;
    GameObject[] others;
    float _timeToFindGold = 60f;
    bool _activateTime = false;
    public GameObject RimZombie;
    public GameObject FogZombies;
    public GameObject[] RimQuestThings;
    public GameObject Player;
    public GameObject[] kids;
    int kidCount = 0;
    public GameObject LastLocation;
    public GameObject DownStairs;
    public GameObject Blocker;
    public GameObject QuestPanel;
    public Text ActiveList;
    public Text DoneList;
    QuestDescription questDescription = new QuestDescription();
    bool prolog = true;
    int prologpart = 0;
    int prologMaxPart = 2;
    List<StringBuilder> prologueText = new List<StringBuilder>();
    GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        //Player.SetActive(false);
       // FogZombies.SetActive(false);
        RimZombie.SetActive(false);
        others = GameObject.FindGameObjectsWithTag("actor");
        missions = new List<string>();
        missionStatuses = new List<bool>();
        foreach (GameObject go in others)
        {
            go.SetActive(false);
        }
        foreach (var item in kids)
        {
            item.SetActive(false);
        }

        foreach (var item in RimQuestThings)
        {
            item.SetActive(false);
        }
        DownStairs.SetActive(false);
        Blocker.SetActive(false);

        //to do: fill prologue text
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Rodzinna wioska Luny");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Z mojej rodzinnej wsi dochodziły plotki o nieznanej chorobie.");
        stringBuilder.AppendLine("Po pewnym czasie wszyscy mieszkańcy nagle zniknęli.");
        stringBuilder.AppendLine("Jako dowódca dostałam rozkaz spalenia całej miejscowości co miało uspokoić sytuację na wyspie.");
        stringBuilder.AppendLine("Tak też zrobiłam.");
        prologueText.Add(stringBuilder);

        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.AppendLine("Miasto Rhodi położone najbliżej rodzinnej wsi");
        stringBuilder1.AppendLine();
        stringBuilder1.AppendLine("Arthemon(namiestnik miasta): Wezwałem cię Luna, gdyż doszły nas słuchy o kolejnym przypadku.");
        stringBuilder1.AppendLine("Nie mogę tego tak zostawić. Pojedziesz do wsi Ymera i sprawdzisz czy plotki są prawdziwe");
        stringBuilder1.AppendLine("Prześlij informację jak najszybciej, sytuacja staje się nieprzyjemna.");
        prologueText.Add(stringBuilder1);

        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.AppendLine("Miejsce akcji: Ymera");
        prologueText.Add(stringBuilder2);
    }

    void Update()
    {
        if(prolog)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ++prologpart;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (QuestPanel.gameObject.activeInHierarchy)
            {
                
                QuestPanel.gameObject.SetActive(false);
               
            }

            else
            {
                
                QuestPanel.gameObject.SetActive(true);
                
            }
            
        }
            
    }

    private void FixedUpdate()
    {
        if (_activateTime)
        {
            _timeToFindGold -= Time.fixedDeltaTime;
            if (_timeToFindGold <= 0)
            {
                foreach (var item in RimQuestThings)
                {
                    item.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("SetAction", "Czas upłynął! Nie udało sie wykonać misji!");
                _activateTime = false;
                GameObject.FindGameObjectWithTag("storyobject").SendMessage("DisableTime");
            }
            else GameObject.FindGameObjectWithTag("storyobject").SendMessage("ShowTimeElapsed", _timeToFindGold);
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        if(prolog)
        {
            if (prologpart < prologueText.Count)
            {
                guiStyle.fontSize = Screen.height / 40;
                //guiStyle.normal.textColor = Color.red;
                //guiStyle.fontStyle = FontStyle.Bold;
                var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                texture.SetPixel(0, 0, new Color(1f, 1f, 1f, 1f));
                texture.Apply();
                GUIContent content = new GUIContent(prologueText[prologpart].ToString());
                Vector2 size = guiStyle.CalcSize(content);
                guiStyle.normal = new GUIStyleState { textColor = Color.black, background = texture };

                //added
                Texture2D targetTexture = new Texture2D(Screen.width, Screen.height);
                //GetComponent<Renderer>().material.mainTexture = targetTexture;

                Color[] fillColorArray = targetTexture.GetPixels();

                for (int i = 0; i < fillColorArray.Length; ++i)
                {
                    Color color = Color.white;
                    color.a = 1f;
                    fillColorArray[i] = color;
                }

                targetTexture.SetPixels(fillColorArray);

                targetTexture.Apply();



                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), targetTexture);
                //added

                GUI.Label(new Rect(Screen.width/2 - size.x/2, Screen.height/3 - size.y/2, Screen.width, Screen.height), content, guiStyle);
            }
            else
            {
                prolog = false;
                Player.SetActive(true);
            }
        }

       string active="";
        string done="";
        
        for (int i = 0; i < missions.Count; i++)
        {
            //GUI.Label(new Rect(10, 10 + 30 * i, 300, 300), missions[i] + " " + missionStatuses[i]);
            if(!missionStatuses[i])
                active += (questDescription.getDescription(missions[i]) + "\n\n");
            else
                done += (questDescription.getDescription(missions[i]) + "\n\n");
        }
        ActiveList.text = active;
        DoneList.text = done;
    }

    public void AddMission(string mission)
    {
        SendMessage("SetImportant", "Zaktualizowano dziennik misji!");
        Debug.Log("adding mission " + mission);
        if (missions.Contains(mission)) return;
        //if both are true then activate Rim
        if (mission == "find_gold")
        {
            _activateRimConditions[0] = true;

        }
        if (mission == "find_children")
        {
            _activateRimConditions[1] = true;
            foreach (var item in kids)
            {
                item.SetActive(true);
            }
        }
        if (mission == "find_moner") ActiveOthers();
        if (mission == "follow_rim") ActivateRimPart2();
        if (mission == "find_gold") ChangeBorenNextId();
        if (mission == "rim_find_gold") ActivateTime();
        if (mission == "talk_to_moner") ActivateLastLocation();

        for (int i = 0; i < missions.Count; i++)
        {
            if (missions[i] == mission) return;
        }
        missions.Add(mission);
        missionStatuses.Add(false);

        //start rim part
        if (_activateRimConditions[0]) StartCoroutine(ActivateRim());
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
        SendMessage("SetImportant", "Zaktualizowano dziennik misji!");
        string mission1;
        string mission2;
        Debug.Log("completing mission " + missionComplete);
        if(missionComplete == "find_childrenfind_moner")
        {
            mission1 = "find_children";
            mission2 = "find_moner";
            for (int i = 0; i < missions.Count; i++)
            {
                if (mission1 == (string)missions[i])
                {
                    missionStatuses[i] = true;
                    break;
                }
            }
            for (int i = 0; i < missions.Count; i++)
            {
                if (mission2 == (string)missions[i])
                {
                    missionStatuses[i] = true;
                    break;
                }
            }
            return;
        }
        if (missionComplete == "rim_find_gold")
        {
            _activateTime = false;
            GameObject.FindGameObjectWithTag("storyobject").SendMessage("DisableTime");
        }
        for (int i = 0; i < missions.Count; i++)
        {
            if (missionComplete == (string)missions[i])
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
        RimZombie.SetActive(true);
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
        foreach (var item in RimQuestThings)
        {
            item.SetActive(true);
        }
        Rim.GetComponent<EnemySight>().enabled = true;
    }

    void ActiveOthers()
    {
        foreach (GameObject go in others)
        {
            if (go != gameObject) go.SetActive(true);
        }
    }

    void ActivateTime()
    {
        _activateTime = true;
        gameObject.SendMessage("SetAction", "Sprawdź czy Rim ma złoto");
        Rim.GetComponent<DialogueWindow>().enabled = false;
        Rim.GetComponent<DialogueActor>().enabled = false;
        Rim.GetComponent<CapsuleCollider>().enabled = false;
    }

    void ActivateLastLocation()
    {
        FogZombies.SetActive(true);
        DownStairs.SetActive(true);
        Indicator.SendMessage("SetIndicatedLocation", LastLocation);
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
            if (String.Equals(mission, "find_gold"))
            {
                GameObject boren = others.Select(e => e).Where(e => e.GetComponent<DialogueActor>().actorName == "Boren").ToArray()[0];
                boren.SendMessage("ChangeNextId", "1LB");
            }
        }
    }

    public void EndTheGame()
    {
        Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().CanMove = false;
        ShowEnd();
    }

    void ShowEnd()
    {
        Player.SendMessage("EndTheGame");
    }

    public void KidFound()
    {
        ++kidCount;
        if(kidCount == 3)
        {
            CompleteMission("find_children");
        }
    }

    public void ActivateBlocker()
    {
        if (DownStairs.activeSelf)
        {
            Blocker.SetActive(true);
            Player.SendMessage("HaveSecondChance");
        }
    }

    public void SecondChance()
    {
        Player.transform.SetPositionAndRotation(LastLocation.transform.position, Player.transform.rotation);
        //Player.SendMessage("Alive");
    }
}
