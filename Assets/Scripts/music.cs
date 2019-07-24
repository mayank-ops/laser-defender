using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {

    private void Awake()
    {
        int count = FindObjectsOfType<music>().Length; // FindObjectsOfType(GetType()).Length;
        if(count>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
