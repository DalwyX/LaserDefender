using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("General")]
    [SerializeField] float health = 300f;
    [SerializeField] float maxHealth = 1000f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserShootPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] Vector3 projectileSpawnOffset = new Vector3(0, 0.5f, 0);

    [Header("Sound Effects")]
    [SerializeField] AudioClip playerShotSound;
    [SerializeField] [Range(0, 1)] float playerShotVolume = 1f;
    [SerializeField] AudioClip playerDieSound;
    [SerializeField] [Range(0, 2)] float playerDieVolume = 1f;

    [Header("Extra")]
    [SerializeField] GameObject superLaserPrefab;
    [SerializeField] float superLaserDuration = 10f;
    [SerializeField] float superLaserSpeed = 20f;
    [SerializeField] float superLaserFiringPeriod = 0.05f;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] float shieldDuration = 10f;

    public float Health
    {
        get { return health; }
    }

    bool isAmmoActive = false;
    bool isShieldActive = false;

    float activeLaserSpeed;
    float activeFiringPeriod;

    GameSession gameSession;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    GameObject activeLasers;
    Coroutine firingCoroutine;

    void Start ()
    {
        gameSession = FindObjectOfType<GameSession>();
        SetupMoveBoundries();
        activeLasers = laserShootPrefab;
        activeLaserSpeed = projectileSpeed;
        activeFiringPeriod = projectileFiringPeriod;

        if (laserShootPrefab == null)
        {
            Debug.LogError("Отсутсвует объект laser shoot");
        }
	}

    void Update ()
    {
        if (gameSession == null)
            gameSession = FindObjectOfType<GameSession>();
        Move();
        Fire();
        ExtraActions();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Health Bonus")
        {
            collision.GetComponent<Bonus>().PickUp();
            if (health <= maxHealth - 50f)
                health += 50f;
        }
        else if (collision.tag == "PowerUp Bonus")
        {
            collision.GetComponent<Bonus>().PickUp();
            gameSession.AddSuperAmmo();
        }
        else if (collision.tag == "Shield Bonus")
        {
            collision.GetComponent<Bonus>().PickUp();
            gameSession.AddShield();
        }
        else
        {
            DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer, (collision.tag == "Boss"));
        }
    }

    void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1") && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    void ExtraActions()
    {
        if (Input.GetButtonDown("Super Ammo") && !isAmmoActive && gameSession.SuperAmmos > 0)
        {
            StartCoroutine(ActivateSuperAmmo());
        }
        if (Input.GetButtonDown("Shield") && !isShieldActive && gameSession.Shields > 0)
        {
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject laser = Instantiate(activeLasers, transform.position + projectileSpawnOffset, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, activeLaserSpeed);
            AudioSource.PlayClipAtPoint(playerShotSound, Camera.main.transform.position, playerShotVolume);
            yield return new WaitForSeconds(activeFiringPeriod);
        }
    }

    IEnumerator ActivateSuperAmmo()
    {
        isAmmoActive = true;
        gameSession.UseSuperAmmo();
        activeLasers = superLaserPrefab;
        activeLaserSpeed = superLaserSpeed;
        activeFiringPeriod = superLaserFiringPeriod;
        yield return new WaitForSeconds(superLaserDuration);
        activeLasers = laserShootPrefab;
        activeLaserSpeed = projectileSpeed;
        isAmmoActive = false;
        activeFiringPeriod = projectileFiringPeriod;
    }

    IEnumerator ActivateShield()
    {
        isShieldActive = true;
        gameSession.UseShield();
        GameObject shieldObject = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        yield return new WaitForSeconds(shieldDuration);
        Destroy(shieldObject);
        isShieldActive = false;
    }

    void ProcessHit(DamageDealer damageDealer, bool isBoss)
    {
        health -= damageDealer.GetDamage();
        if (!isBoss)
            damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
            AudioSource.PlayClipAtPoint(playerDieSound, Camera.main.transform.position, playerDieVolume);
            Destroy(gameObject);
        }
    }

    void SetupMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}

