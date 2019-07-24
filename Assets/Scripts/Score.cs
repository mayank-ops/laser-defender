using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text mytext;
    GameSession obj;

	// Use this for initialization
	void Start ()
    {
        mytext = GetComponent<Text>();
        obj = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        mytext.text = obj.getScore().ToString();
	}
}
