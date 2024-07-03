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
    private bool waveInProgress = false;
    
    void Start()
    {
        
        StartCoroutine(ManageWaves());
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
        int enemiesToSpawn = Mathf.RoundToInt(enemiesPerWave * Mathf.Pow(difficultyMultiplier, currentWave - 1));

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
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Transform spawnPoint = m_spawnPoints[randomIndex];

            if (spawnPoint != null)
            {
                var enemyAI = Instantiate(m_EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
                // EnemyAi enemyAI = newEnemy.GetComponent<EnemyAi>();

                if (enemyAI != null)
                {
                    enemyAI._health = Mathf.RoundToInt(enemyAI._health * Mathf.Pow(difficultyMultiplier, currentWave - 1));
                    NavMeshAgent agent = enemyAI.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.speed *= Mathf.Pow(difficultyMultiplier, currentWave - 1);
                    }
                }

                enemyCount++;
            }
        }
    }
}
