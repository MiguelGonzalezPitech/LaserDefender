using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        
    }


    // Update is called once per frame
    void Update()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText.text = gameSession.GetScore().ToString();
    }
}
