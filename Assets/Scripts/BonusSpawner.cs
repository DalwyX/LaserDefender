using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {
    
	[Header("Health Bonus")]
    [SerializeField] GameObject healthPrefab;
    [SerializeField] [Range(0, 100)] int healthRarity = 10;
    Vector2 healthProb;

    [Header("Super Ammo Bonus")]
    [SerializeField] GameObject superAmmoPrefab;
    [SerializeField] [Range(0, 100)] int superAmmoRarity = 5;
    Vector2 ammoProb;

    [Header("Shield Bonus")]
    [SerializeField] GameObject sheildPrefab;
    [SerializeField] [Range(0, 100)] int shieldRarity = 5;
    Vector2 shieldProb;

    void Start()
    {
        float rarityCount = 0;
        healthProb = new Vector2(rarityCount / 100, (float)healthRarity / 100);
        rarityCount += healthRarity;
        ammoProb = new Vector2(rarityCount / 100, rarityCount / 100 + (float)superAmmoRarity / 100);
        rarityCount += superAmmoRarity;
        shieldProb = new Vector2(rarityCount / 100, rarityCount / 100 + (float)shieldRarity / 100);
    }

    public void GetBonus(Vector3 position)
    {
        float randomNumber = Random.Range(0, 1f);
        if (randomNumber >= healthProb.x && randomNumber <= healthProb.y)
        {
            HealthBonus(position);
        }
        else if (randomNumber >= ammoProb.x && randomNumber <= ammoProb.y)
        {
            AmmoBonus(position);
        }
        else if (randomNumber >= shieldProb.x && randomNumber <= shieldProb.y)
        {
            ShieldBonus(position);
        }
    }

    void HealthBonus(Vector3 position)
    {
        GameObject newBonus = Instantiate(healthPrefab, position, Quaternion.identity) as GameObject;
        newBonus.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
    }

    void AmmoBonus(Vector3 position)
    {
        GameObject newBonus = Instantiate(superAmmoPrefab, position, Quaternion.identity) as GameObject;
        newBonus.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
    }

    void ShieldBonus(Vector3 position)
    {
        GameObject newBonus = Instantiate(sheildPrefab, position, Quaternion.identity) as GameObject;
        newBonus.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
    }
}
