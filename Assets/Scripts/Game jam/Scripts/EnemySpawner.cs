using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool objectPool;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    public int baseEnemiesPerWave = 10;
    public float baseSpawnInterval = 2.0f;
    public float waveInterval = 10.0f;

    private int currentWave = 0;
    private int enemiesSpawned;
    private float waveTimer;
    private bool bossActive = false;
    private float totalTimeElapsed;

    private void Update()
    {
        totalTimeElapsed += Time.deltaTime;
        if (!bossActive)
        {
            waveTimer += Time.deltaTime;

            if (waveTimer >= waveInterval)
            {
                waveTimer = 0f;
                StartNewWave();
            }
        }
    }

    private void StartNewWave()
    {
        if (bossActive) return;

        currentWave++;
        enemiesSpawned = 0;

        if (currentWave % 5 == 0)
        {
            SpawnBoss();
        }
        else
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesPerWave = Mathf.FloorToInt(baseEnemiesPerWave * (1 + totalTimeElapsed / 360.0f));
        float spawnInterval = Mathf.Max(baseSpawnInterval * (1 - totalTimeElapsed / 300.0f), 0.5f);

        while (enemiesSpawned < enemiesPerWave)
        {
            if (bossActive) yield break;

            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, objectPool.enemyPrefabs.Length);
        GameObject enemy = objectPool.GetPooledObject(enemyIndex);
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.SetActive(true);
    }

    private void SpawnBoss()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject boss = Instantiate(bossPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        boss.SetActive(true);

        Boss bossComponent = boss.GetComponent<Boss>();
        if (bossComponent != null)
        {
            bossComponent.OnBossDeath += ResumeSpawning;
        }

        bossActive = true;
    }

    private void ResumeSpawning()
    {
        bossActive = false;
        waveTimer = 0f;
        StartNewWave();
    }

}

