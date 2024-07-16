using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public EnemyAi m_EnemyPrefab;
    
    public float spawnInterval = 1f; 
    public int enemiesPerWave = 5;
    public int enemyCount = 0;
   
    public float difficultyMultiplier = 1.1f;
    public int currentWave = 0;
    public float timeBetweenWaves = 5f;
    private  bool waveInProgress = false;
    
    void Start()
    {
        
        StartCoroutine(ManageWaves());
        GameManager.Instance.player.PlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        waveInProgress = false;
        StopAllCoroutines();
    }

    //TODO will be converted to Update func  
    private IEnumerator ManageWaves()
    {
        while (true)
        {
            if (!waveInProgress)
            {
                waveInProgress = true;
                yield return StartCoroutine(SpawnWave());
                yield return new WaitForSeconds(timeBetweenWaves);   
                waveInProgress = false;
                
            }
        }
        yield return null;
    }

    private IEnumerator SpawnWave()
    {
        currentWave++;
        Debug.Log("current wave " + currentWave);
        int enemiesToSpawn = 20;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemyCount < enemiesPerWave )
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    private void SpawnNewEnemy()
    {
        if (m_EnemyPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
        {
            Debug.Log("Spawn points length: " + m_spawnPoints.Length);//
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Debug.Log("Random index: " + randomIndex);//
            
            EnemyObjectPool.SharedInstance.CreateEnemyAtFirstStart();
            Transform spawnPoint = m_spawnPoints[randomIndex % m_spawnPoints.Length];
            var enemyAi = EnemyObjectPool.SharedInstance.GetPooledObject();
            if (enemyAi != null)
            {
                enemyAi.transform.position = spawnPoint.position;
                enemyAi.transform.rotation = spawnPoint.rotation;
                enemyAi.SetActive(true);

                EnemyAi enemyAiComponent = enemyAi.GetComponent<EnemyAi>();
                if (enemyAiComponent != null)
                {
                    enemyAiComponent.ResetEnemy();
                    enemyAiComponent._health = Mathf.RoundToInt(enemyAiComponent._health * Mathf.Pow(difficultyMultiplier, currentWave - 1));
                }
                
                NavMeshAgent agent = enemyAiComponent.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.speed *= Mathf.Pow(difficultyMultiplier, currentWave - 1);
                }
            }
            
            enemyCount++;
        }
        else
        {
            Debug.Log("Spawn points array is null or empty.");
        }
    }
}
   