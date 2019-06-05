using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float health = 100f;
    [SerializeField] float speed = 1f;
    [SerializeField] bool boss = false;
    [SerializeField] int winPoints = 100;
    [SerializeField] GameObject explosionVFX;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] Vector3 projectileSpawnOffset = new Vector3(0, 0.5f, 0);

    [Header("Sound Effects")]
    [SerializeField] AudioClip enemyShotSound;
    [SerializeField] [Range(0, 1)] float enemyShotVolume = 1f;
    [SerializeField] AudioClip enemyDieSound;
    [SerializeField] [Range(0, 2)] float enemyDieVolume = 1f;

    public float Speed
    {
        get { return speed; }
    }
    public bool Boss
    {
        get { return boss; }
    }

    BonusSpawner bonus;
    GameSession gameSession;
    float shotCounter;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        bonus = FindObjectOfType<BonusSpawner>();

        if (boss)
        {
            FindObjectOfType<MusicPlayer>().TurnOnBossTheme();
        }
    }

    void Update()
    {
        if (gameSession == null)
            gameSession = FindObjectOfType<GameSession>();
        CountDownAndShoot();
    }

    void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position - projectileSpawnOffset, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyShotSound, Camera.main.transform.position, enemyShotVolume);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!boss)
            {
                AudioSource.PlayClipAtPoint(enemyDieSound, Camera.main.transform.position, enemyDieVolume);
                Explode();
            }
        }
        else
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            gameSession.UpdateScore(winPoints);
            AudioSource.PlayClipAtPoint(enemyDieSound, Camera.main.transform.position, enemyDieVolume);
            Explode();
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        bonus.GetBonus(transform.position);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation) as GameObject;
        Destroy(explosion.gameObject, 1f);
    }
}
