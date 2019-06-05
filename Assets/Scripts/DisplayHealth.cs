using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour {

    Player player;
    Text textField;

    void Start()
    {
        player = FindObjectOfType<Player>();
        textField = GetComponent<Text>();
    }

    void Update()
    {
        textField.text = GetHealth();
    }

    string GetHealth()
    {
        float playerHealth = player.Health;
        playerHealth = (playerHealth < 0) ? 0 : playerHealth;
        int playerLenght = playerHealth.ToString().Length;
        string formedHealth = "";
        if (playerHealth.ToString().Length < 3)
        {
            for (int i = 0; i < (3 - playerLenght); i++)
            {
                formedHealth += "0";
            }
        }

        return formedHealth + playerHealth.ToString();
    }

}
