using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int score = 0;

    [SerializeField]
    float playerHealth = 200; 
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

    public void AddDamage(float damage)
    {
        playerHealth -= damage;
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
