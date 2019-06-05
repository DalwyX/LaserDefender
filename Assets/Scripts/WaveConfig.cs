using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [Header("General")]
    [SerializeField] float startDelay = 0;
    [SerializeField] bool endDelay = true;

    [Header("Thread 1")]
    [SerializeField] GameObject firstThreadEnemy;
    [SerializeField] GameObject firstThreadPath;
    [SerializeField] int firstThreadNumberOfEnemies = 5;
    [SerializeField] float firstThreadStartingDelay = 0;
    [SerializeField] float firstThreadSpawnDelay = 0.5f;

    [Header("Thread 2")]
    [SerializeField] bool secondThreadIsActive = false;
    [SerializeField] GameObject secondThreadEnemy;
    [SerializeField] GameObject secondThreadPath;
    [SerializeField] int secondThreadNumberOfEnemies = 5;
    [SerializeField] float secondThreadStartingDelay = 0;
    [SerializeField] float secondThreadSpawnDelay = 0.5f;

    [Header("Thread 3")]
    [SerializeField] bool thirdThreadIsActive = false;
    [SerializeField] GameObject thirdThreadEnemy;
    [SerializeField] GameObject thirdThreadPath;
    [SerializeField] int thirdThreadNumberOfEnemies = 5;
    [SerializeField] float thirdThreadStartingDelay = 0;
    [SerializeField] float thirdThreadSpawnDelay = 0.5f;

    [Header("Thread 4")]
    [SerializeField] bool fourthThreadIsActive = false;
    [SerializeField] GameObject fourthThreadEnemy;
    [SerializeField] GameObject fourthThreadPath;
    [SerializeField] int fourthThreadNumberOfEnemies = 5;
    [SerializeField] float fourthThreadStartingDelay = 0;
    [SerializeField] float fourthThreadSpawnDelay = 0.5f;


    // General Getters
    public float StartDelay
    {
        get { return startDelay; }
    }
    public bool EndDelay
    {
        get { return endDelay; }
    }

    // Thread 1 Getters
    public bool IsFirstThreadActive
    {
        get
        {
            if (firstThreadEnemy != null && firstThreadPath != null && firstThreadNumberOfEnemies > 0)
                return true;
            else
            {
                Debug.LogError("Первый поток не настроен");
                return false;
            }
        }
    }
    public GameObject FirstThreadEnemy
    {
        get { return firstThreadEnemy; }
    }
    public List<Transform> FirstThreadPath
    {
        get
        {
            var waveWaypoints = new List<Transform>();

            foreach (Transform waypoint in firstThreadPath.transform)
            {
                waveWaypoints.Add(waypoint);
            }

            return waveWaypoints;
        }
    }
    public int FirstThreadNumberOfEnemies
    {
        get { return firstThreadNumberOfEnemies; }
    }
    public float FirstThreadStartingDelay
    {
        get { return firstThreadStartingDelay; }
    }
    public float FirstThreadSpawnDelay
    {
        get { return firstThreadSpawnDelay; }
    }

    // Thread 2 Getters
    public bool IsSecondThreadActive
    {
        get
        {
            if (secondThreadIsActive)
            {
                if (secondThreadEnemy != null && secondThreadPath != null && secondThreadNumberOfEnemies > 0)
                    return true;
                else
                {
                    Debug.LogError("Второй поток включен, но не настроен");
                    return false;
                }
            }
            else return false;
        }
    }
    public GameObject SecondThreadEnemy
    {
        get { return secondThreadEnemy; }
    }
    public List<Transform> SecondThreadPath
    {
        get
        {
            var waveWaypoints = new List<Transform>();

            foreach (Transform waypoint in secondThreadPath.transform)
            {
                waveWaypoints.Add(waypoint);
            }

            return waveWaypoints;
        }
    }
    public int SecondThreadNumberOfEnemies
    {
        get { return secondThreadNumberOfEnemies; }
    }
    public float SecondThreadStartingDelay
    {
        get { return secondThreadStartingDelay; }
    }
    public float SecondThreadSpawnDelay
    {
        get { return secondThreadSpawnDelay; }
    }

    // Thread 3 Getters
    public bool IsThirdThreadActive
    {
        get
        {
            if (thirdThreadIsActive)
            {
                if (thirdThreadEnemy != null && thirdThreadPath != null && thirdThreadNumberOfEnemies > 0)
                    return true;
                else
                {
                    Debug.LogError("Третий поток включен, но не настроен");
                    return false;
                }
            }
            else return false;
        }
    }
    public GameObject ThirdThreadEnemy
    {
        get { return thirdThreadEnemy; }
    }
    public List<Transform> ThirdThreadPath
    {
        get
        {
            var waveWaypoints = new List<Transform>();

            foreach (Transform waypoint in thirdThreadPath.transform)
            {
                waveWaypoints.Add(waypoint);
            }

            return waveWaypoints;
        }
    }
    public int ThirdThreadNumberOfEnemies
    {
        get { return thirdThreadNumberOfEnemies; }
    }
    public float ThirdThreadStartingDelay
    {
        get { return thirdThreadStartingDelay; }
    }
    public float ThirdThreadSpawnDelay
    {
        get { return thirdThreadSpawnDelay; }
    }

    // Thread 4 Getters
    public bool IsFourthThreadActive
    {
        get
        {
            if (fourthThreadIsActive)
            {
                if (fourthThreadEnemy != null && fourthThreadPath != null && fourthThreadNumberOfEnemies > 0)
                    return true;
                else
                {
                    Debug.LogError("Четвертый поток включен, но не настроен");
                    return false;
                }
            }
            else return false;
        }
    }
    public GameObject FourthThreadEnemy
    {
        get { return fourthThreadEnemy; }
    }
    public List<Transform> FourthThreadPath
    {
        get
        {
            var waveWaypoints = new List<Transform>();

            foreach (Transform waypoint in fourthThreadPath.transform)
            {
                waveWaypoints.Add(waypoint);
            }

            return waveWaypoints;
        }
    }
    public int FourthThreadNumberOfEnemies
    {
        get { return fourthThreadNumberOfEnemies; }
    }
    public float FourthThreadStartingDelay
    {
        get { return fourthThreadStartingDelay; }
    }
    public float FourthThreadSpawnDelay
    {
        get { return fourthThreadSpawnDelay; }
    }
}
