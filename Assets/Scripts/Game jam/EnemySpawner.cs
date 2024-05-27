using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool objectPool; // Reference to the ObjectPool script
    public GameObject bossPrefab;
    public Transform[] spawnPoints;
    public int enemiesPerWave = 10;
    public float spawnInterval = 2.0f;
    public float waveInterval = 10.0f;

    private int currentWave = 0;
    private int enemiesSpawned;
    private float waveTimer;
    private float spawnTimer;

    void Update()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer >= waveInterval)
        {
            waveTimer = 0f;
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        currentWave++;
        enemiesSpawned = 0;

        if (currentWave % 5 == 0)
        {
            SpawnBoss();
        }
        else
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemiesSpawned >= enemiesPerWave)
        {
            CancelInvoke("SpawnEnemy");
            return;
        }

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, objectPool.enemyPrefabs.Length);
        GameObject enemy = objectPool.GetPooledObject(enemyIndex);
        enemy.transform.position = spawnPoints[spawnIndex].position;
        enemy.SetActive(true);
        enemiesSpawned++;
    }

    void SpawnBoss()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject boss = Instantiate(bossPrefab);
        boss.transform.position = spawnPoints[spawnIndex].position;
        boss.SetActive(true);
    }
}

