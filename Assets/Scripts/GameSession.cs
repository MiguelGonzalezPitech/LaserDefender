using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        SetupSingleton();
    }

    void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }



    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
