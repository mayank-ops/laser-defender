﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {

    Text mytext;
    player myplayer;

	// Use this for initialization
	void Start ()
    {
        mytext = GetComponent<Text>();
        myplayer = FindObjectOfType<player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mytext.text = myplayer.getHealth().ToString();
	}
}
