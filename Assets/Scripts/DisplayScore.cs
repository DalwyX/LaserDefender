using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

    GameSession gameSession;
    Text textField;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        textField = GetComponent<Text>();
    }

    void Update()
    {
        textField.text = GetScore();
    }

    string GetScore()
    {
        if (gameSession == null)
            gameSession = FindObjectOfType<GameSession>();
        float gameScore = gameSession.GameScore;
        int scoreLenght = gameScore.ToString().Length;
        string formedScore = "";
        if (gameScore.ToString().Length < 6)
        {
            for (int i = 0; i < (6 - scoreLenght); i++)
            {
                formedScore += "0";
            }
        }
        return formedScore + gameScore.ToString();
    }

}
