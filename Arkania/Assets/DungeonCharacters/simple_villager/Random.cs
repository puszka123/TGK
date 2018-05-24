using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Random : MonoBehaviour
{
    public static System.Random RandomGen { get; set; }
    // Use this for initialization
    void Awake()
    {
        if(RandomGen == null)
        RandomGen = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
