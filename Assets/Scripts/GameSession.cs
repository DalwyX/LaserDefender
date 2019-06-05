using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] float gameScore;
    [SerializeField] int superAmmos = 0;
    [SerializeField] int shields = 0;

    public float GameScore
    {
        get { return gameScore; }
    }
    public int SuperAmmos
    {
        get { return superAmmos; }
    }
    public int Shields
    {
        get { return shields; }
    }

    void Awake()
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

    public void AddSuperAmmo()
    {
        if (superAmmos < 3)
            superAmmos++;
    }

    public void UseSuperAmmo()
    {
        superAmmos--;
    }

    public void AddShield()
    {
        if (shields < 3)
            shields++;
    }

    public void UseShield()
    {
        shields--;
    }


    public void ResetGame()
    {
        gameScore = 0;
        superAmmos = 0;
        shields = 0;
    }

    public void UpdateScore(int points = 100)
    {
        gameScore += points;
    }

}
