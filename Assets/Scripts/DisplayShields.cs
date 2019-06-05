using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayShields : MonoBehaviour {

    GameSession gameSession;
    Image[] shields;

	void Start ()
    {
        gameSession = FindObjectOfType<GameSession>();
        shields = GetComponentsInChildren<Image>();
	}

    void Update()
    {
        SetShields();
    }

    void SetShields()
    {
        if (gameSession == null)
            gameSession = FindObjectOfType<GameSession>();
        int shieldNumber = gameSession.Shields;
        switch (shieldNumber)
        {
            case 0:
                shields[1].gameObject.SetActive(false);
                shields[2].gameObject.SetActive(false);
                shields[3].gameObject.SetActive(false);
                break;
            case 1:
                shields[1].gameObject.SetActive(true);
                shields[2].gameObject.SetActive(false);
                shields[3].gameObject.SetActive(false);
                break;
            case 2:
                shields[1].gameObject.SetActive(true);
                shields[2].gameObject.SetActive(true);
                shields[3].gameObject.SetActive(false);
                break;
            case 3:
                shields[1].gameObject.SetActive(true);
                shields[2].gameObject.SetActive(true);
                shields[3].gameObject.SetActive(true);
                break;
        }
    }
}