using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class QuestDescription  {
    

    Dictionary<string, string> dictionary = new Dictionary<string, string>();

    public QuestDescription()
    {
        dictionary.Add("find_moner", "Znajdź Monera: Muszę odnaleźć Monera.");
        dictionary.Add("find_children", "Znajdź dzieci Borena: Muszę znaleźć trójkę dzieci. Dzecinnie proste zadanie.");
        dictionary.Add("find_gold", "Znajdź złoto Hermera: Kto ukradł jego złoto?");
        dictionary.Add("follow_rim", "Podążaj za Rimem: Może on coś wie...");
        dictionary.Add("rim_find_gold", "Sprawdź Rima: Sprawdzę czy to Rim nie ukradł złota.");
        dictionary.Add("boren_debt", "Dług Borena: Spłacę jego dług");
        dictionary.Add("talk_to_moner", "Porozmawiaj z Monerem: Najpierw muszę znaleźć wejście w tym mglistym lesie.");
    }



    public string getDescription( string questName)
    {
        if (dictionary.ContainsKey(questName))
        {
            return dictionary[questName];
        }
        else return "Misja: brak opisu misji.";
    }
}
