using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAmmos : MonoBehaviour {

    GameSession gameSession;
    Image[] ammos;

	void Start ()
    {
        gameSession = FindObjectOfType<GameSession>();
        ammos = GetComponentsInChildren<Image>();
	}

    void Update()
    {
        SetAmmos();
    }

    void SetAmmos()
    {
        if (gameSession == null)
            gameSession = FindObjectOfType<GameSession>();
        int ammoNumber = gameSession.SuperAmmos;
        switch (ammoNumber)
        {
            case 0:
                ammos[1].gameObject.SetActive(false);
                ammos[2].gameObject.SetActive(false);
                ammos[3].gameObject.SetActive(false);
                break;
            case 1:
                ammos[1].gameObject.SetActive(true);
                ammos[2].gameObject.SetActive(false);
                ammos[3].gameObject.SetActive(false);
                break;
            case 2:
                ammos[1].gameObject.SetActive(true);
                ammos[2].gameObject.SetActive(true);
                ammos[3].gameObject.SetActive(false);
                break;
            case 3:
                ammos[1].gameObject.SetActive(true);
                ammos[2].gameObject.SetActive(true);
                ammos[3].gameObject.SetActive(true);
                break;
        }
    }
}