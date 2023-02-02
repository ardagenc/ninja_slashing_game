using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavespawner : MonoBehaviour
{
    public enum SpawnState
    {
        Waiting,
        Spawning,
    }

    public SpawnState state = SpawnState.Waiting;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public int enemyCount = 0;
    public int enemyCountMax;

    public int deadEnemyCount = 0;
    public int increaseDeadEnemyCountMax;

    [SerializeField] GameObject[] enemyPrefabs;    
    private int randomEnemyPrefab;    
    private Vector2 spawnPosition;
    private int randomPositionX;
    private int randomPositionY;

    void Start()
    {
        waveCountdown = timeBetweenWaves;

        
    }

    
    void Update()
    {
        if(state == SpawnState.Waiting)
        {
            waveCountdown -= Time.deltaTime;
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.Spawning;

        if(increaseDeadEnemyCountMax >= 3)
        {
            enemyCountMax += increaseDeadEnemyCountMax / 3;
            increaseDeadEnemyCountMax = 0;

            if(timeBetweenWaves > 0.2f)
            {
                timeBetweenWaves -= 0.05f;
            }
            
        } 
        
        if (enemyCount < enemyCountMax)
        {
            SpawnEnemy();
            enemyCount++;
        }

        waveCountdown = timeBetweenWaves;
        
        state = SpawnState.Waiting;

        yield break;
    }
    void SpawnEnemy()
    {
        randomEnemyPrefab = Random.Range(0, enemyPrefabs.Length);

        randomPositionX = Random.Range(-10, 10);
        randomPositionY = Random.Range(-4, 4);

        spawnPosition = new Vector2(randomPositionX, randomPositionY);

        GameObject newEnemy = Instantiate(enemyPrefabs[randomEnemyPrefab], spawnPosition, Quaternion.identity);
    }
}
