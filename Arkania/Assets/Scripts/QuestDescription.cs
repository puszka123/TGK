using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class QuestDescription  {
    

    Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public QuestDescription()
    {
        dictionary.Add("find_moner", "Musze odnaleść monera, pewnie się gdzieś zapił i śpi. Dlaczego ja musze robić takie rzeczy?");
        
    }



    public string getDescription( string questName)
    {


        if (questName == "find_moner")
        {
            return dictionary[questName];
        }
        return "przykładowy opis zadania przykładowy opis zadania ";

    }
}
