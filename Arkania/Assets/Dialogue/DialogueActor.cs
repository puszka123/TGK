using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogueActor : MonoBehaviour {

    public string actorName;
    public string dialoguePath;

    List<List<string>> dialogueOptionSets = new List<List<string>>();
    List<List<string>> dialogueActions = new List<List<string>>();

    List<string> optionSetIds = new List<string>();
    List<string> optionSetQuestions = new List<string>();

    // Use this for initialization
    void Start () {
        XmlDocument doc = new XmlDocument();
        doc.Load(dialoguePath);

        XmlNode root = doc.GetElementsByTagName("Dialogue")[0];


        XmlNodeList optionSets = root.SelectNodes("OptionSet");
        foreach (XmlNode optionSet in optionSets)
        {
            XmlNode optionSetId = optionSet.SelectNodes("OptionSetId")[0];
            //Debug.Log("option set id = " + optionSetId.InnerText);
            optionSetIds.Add(optionSetId.InnerText);
            XmlNode dialogueQuestion = optionSet.SelectNodes("Question")[0];
            //Debug.Log("question = " + dialogueQuestion.InnerText);
            optionSetQuestions.Add(dialogueQuestion.InnerText);
            XmlNodeList dialogueOptions = optionSet.SelectNodes("Option");

            List<string> options = new List<string>();
            List<string> actions = new List<string>();


            foreach (XmlNode dialogueOption in dialogueOptions)
            {
                XmlNodeList dialogueOptionTexts = dialogueOption.SelectNodes("Text");

                foreach (XmlNode dialogueOptionText in dialogueOptionTexts)
                {
                    //Debug.Log("option = " + dialogueOption.InnerText);
                    options.Add(dialogueOptionText.InnerText);
                }


                XmlNodeList dialogueOptionActions = dialogueOption.SelectNodes("Action");
                string action = "";
                foreach (XmlNode dialogueOptionAction in dialogueOptionActions)
                {
                    action += dialogueOptionAction.InnerText;
                }
                actions.Add(action);


            }
            dialogueOptionSets.Add(options);
            dialogueActions.Add(actions);


        }
    }

    public List<List<string>> Dialogue
    {
        get { return dialogueOptionSets; }
        set { dialogueOptionSets = value; }
    }

    public List<List<string>> Actions
    {
        get { return dialogueActions; }
        set { dialogueActions = value; }
    }

    public List<string> Questions
    {
        get { return optionSetQuestions; }
        set { optionSetQuestions = value; }
    }

    public int getOptionSetById(string id)
    {
        for(int i = 0; i < optionSetIds.Count; i++)
        {
            if (optionSetIds[i].Equals(id)) return i;
        }
        return -1;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
