using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    bool firstThreadIsOver;
    bool secondThreadIsOver;
    bool thirdThreadIsOver;
    bool fourthThreadIsOver;

    IEnumerator Start ()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        yield return new WaitUntil(() => NumberOfActiveEnemies() == 0);
        FindObjectOfType<Level>().LoadNextLevel();
	}

    IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            Debug.Log("Начало " + (i+1) + " волны");
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllThreadsInWave(currentWave));

        }
    }

    IEnumerator SpawnAllThreadsInWave(WaveConfig waveConfig)
    {
        yield return new WaitForSeconds(waveConfig.StartDelay);

        firstThreadIsOver = true;
        secondThreadIsOver = true;
        thirdThreadIsOver = true;
        fourthThreadIsOver = true;

        if (waveConfig.IsFirstThreadActive)
        {
            StartCoroutine(SpawnAllEnemiesInThread(waveConfig.FirstThreadEnemy, waveConfig.FirstThreadPath, waveConfig.FirstThreadNumberOfEnemies, waveConfig.FirstThreadStartingDelay, waveConfig.FirstThreadSpawnDelay, 1));
            firstThreadIsOver = false;
        }
        if (waveConfig.IsSecondThreadActive)
        {
            StartCoroutine(SpawnAllEnemiesInThread(waveConfig.SecondThreadEnemy, waveConfig.SecondThreadPath, waveConfig.SecondThreadNumberOfEnemies, waveConfig.SecondThreadStartingDelay, waveConfig.SecondThreadSpawnDelay, 2));
            secondThreadIsOver = false;
        }
        if (waveConfig.IsThirdThreadActive)
        {
            StartCoroutine(SpawnAllEnemiesInThread(waveConfig.ThirdThreadEnemy, waveConfig.ThirdThreadPath, waveConfig.ThirdThreadNumberOfEnemies, waveConfig.ThirdThreadStartingDelay, waveConfig.ThirdThreadSpawnDelay, 3));
            thirdThreadIsOver = false;
        }
        if (waveConfig.IsFourthThreadActive)
        {
            StartCoroutine(SpawnAllEnemiesInThread(waveConfig.FourthThreadEnemy, waveConfig.FourthThreadPath, waveConfig.FourthThreadNumberOfEnemies, waveConfig.FourthThreadStartingDelay, waveConfig.FourthThreadSpawnDelay, 4));
            fourthThreadIsOver = false;
        }

        if (waveConfig.EndDelay)
        {
            while (!firstThreadIsOver || !secondThreadIsOver || !thirdThreadIsOver || !fourthThreadIsOver)
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitUntil(() => NumberOfActiveEnemies() == 0);
        }
    }

    IEnumerator SpawnAllEnemiesInThread(GameObject enemy, List<Transform> path, int enemyCount, float startingDelay, float timeBetweenSpawns, int threadNumber)
    {
        yield return new WaitForSeconds(startingDelay);
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(enemy, path[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetupPathing(path);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        FinishThread(threadNumber);
    }

    int NumberOfActiveEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        return enemies.Length;
    }

    void FinishThread(int number)
    {
        switch (number)
        {
            case 1:
                firstThreadIsOver = true;
                break;
            case 2:
                secondThreadIsOver = true;
                break;
            case 3:
                thirdThreadIsOver = true;
                break;
            case 4:
                fourthThreadIsOver = true;
                break;
        }
    }

}