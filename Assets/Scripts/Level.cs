using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    GameSession obj;

    private void Start()
    {
        obj = FindObjectOfType<GameSession>();
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LoadMainMenuScreen()
    {
        SceneManager.LoadScene(0);
        obj.resetScore();
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Level-1");
        obj.resetScore();
    }

    public void GameOverScreen()
    {
        StartCoroutine(GameOverCoRoutine());
    }

    IEnumerator GameOverCoRoutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
