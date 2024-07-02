using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public GameObject m_EnemyPrefab;
    public float spawnInterval = 25f;
    public int maxEnemies = 25;
    public int enemyCount = 0;
    void Start()
    {
      StartCoroutine(SpawnEnemies());
    }

    
    private IEnumerator SpawnEnemies()
    {
        while (enemyCount < maxEnemies)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnInterval); // Belirtilen süre kadar bekle
        }
    }


    // private void SpawnNewEnemy()
    // {
    //     int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_spawnPoints.Length - 1));
    //     
    //     Instantiate(m_EnemyPrefab, m_spawnPoints[randomNumber].transform.position, Quaternion.identity);
    // }
    private void SpawnNewEnemy()
    {
        if (m_EnemyPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
        {
            int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_spawnPoints.Length - 1));
            Transform spawnPoint = m_spawnPoints[randomNumber];

            if (spawnPoint != null)
            {
                Instantiate(m_EnemyPrefab, spawnPoint.position, Quaternion.identity);
                enemyCount++; // Increment the enemy counter
            }
        }
    }
    
}
