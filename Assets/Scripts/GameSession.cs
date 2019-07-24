using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    private int currentScore = 0;

    private void Awake()
    {
        if(FindObjectsOfType(GetType()).Length>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void updateScore(int score)
    {
        currentScore += score;
    }

    public int getScore()
    {
        return currentScore;
    }

    public void resetScore()
    {
        Destroy(gameObject);
    }
}
