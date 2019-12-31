using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        var gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGame();
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitForLoadGameOver(1));
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator WaitForLoadGameOver(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Game Over");

    }

}
