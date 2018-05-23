using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Random : MonoBehaviour
{
    public System.Random RandomGen { get; set; }
    // Use this for initialization
    void Start()
    {
        RandomGen = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
