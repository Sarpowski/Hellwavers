using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;
/*
public class EnemyManager : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public EnemyAi m_EnemyPrefab;
    public EnemyBossAi m_BossPrefab;
    
    public float spawnInterval = 1f; 
    public int enemiesPerWave = 5;
    public int enemyCount = 0;
    public float difficultyMultiplier = 1.1f;
    public int currentWave = 0;
    public float timeBetweenWaves = 5f;
    private  bool waveInProgress = false;
    
    void Start()
    {
        EnemyObjectPool.SharedInstance.CreateEnemyAtFirstStart();

        StartCoroutine(ManageWaves());
        GameManager.Instance.player.PlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        waveInProgress = false;
        StopAllCoroutines();
    }

    //TODO will be converted to Update func  
    // private IEnumerator ManageWaves()
    // {
    //     while (true)
    //     {
    //         if (!waveInProgress)
    //         {
    //             waveInProgress = true;
    //             yield return StartCoroutine(SpawnWave());
    //             yield return new WaitForSeconds(timeBetweenWaves);   
    //             waveInProgress = false;
    //             
    //         }
    //     }
    //     yield return null;
    // }
    private IEnumerator ManageWaves()
    {
        while (currentWave < 5)
        {
            waveInProgress = true;
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(timeBetweenWaves);   
            waveInProgress = false;
        }
        // Spawn the boss after the 5th wave
        if (currentWave == 5)
        {
            yield return StartCoroutine(SpawnBoss());
        }

        yield return null;
    }

    private IEnumerator SpawnWave()
    {
        currentWave++;
        Debug.Log("current wave " + currentWave);
        int enemiesToSpawn = enemiesPerWave;
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
    private IEnumerator SpawnBoss()
    {
        Debug.Log("Spawning Boss");
        if (m_BossPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Transform spawnPoint = m_spawnPoints[randomIndex % m_spawnPoints.Length];
            var bossAi = Instantiate(m_BossPrefab, spawnPoint.position, spawnPoint.rotation);
            // Set any special properties for the boss here
        }
        else
        {
            Debug.Log("Spawn points array is null or empty.");
        }

        yield return null;
    }
    // private void SpawnNewEnemy()
    // {
    //     if (m_EnemyPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
    //     {
    //         Debug.Log("Spawn points length: " + m_spawnPoints.Length);//
    //         int randomIndex = Random.Range(0, m_spawnPoints.Length);
    //         Debug.Log("Random index: " + randomIndex);//
    //         
    //         EnemyObjectPool.SharedInstance.CreateEnemyAtFirstStart();
    //         Transform spawnPoint = m_spawnPoints[randomIndex % m_spawnPoints.Length];
    //         var enemyAi = EnemyObjectPool.SharedInstance.GetPooledObject();
    //         if (enemyAi != null)
    //         {
    //             enemyAi.transform.position = spawnPoint.position;
    //             enemyAi.transform.rotation = spawnPoint.rotation;
    //             enemyAi.SetActive(true);
    //
    //             EnemyAi enemyAiComponent = enemyAi.GetComponent<EnemyAi>();
    //             if (enemyAiComponent != null)
    //             {
    //                 enemyAiComponent.ResetEnemy();
    //                 enemyAiComponent._health = Mathf.RoundToInt(enemyAiComponent._health * Mathf.Pow(difficultyMultiplier, currentWave - 1));
    //             }
    //             
    //             NavMeshAgent agent = enemyAiComponent.GetComponent<NavMeshAgent>();
    //             if (agent != null)
    //             {
    //                 agent.speed *= Mathf.Pow(difficultyMultiplier, currentWave - 1);
    //             }
    //         }
    //         
    //         enemyCount++;
    //     }
    //     else
    //     {
    //         Debug.Log("Spawn points array is null or empty.");
    //     }
    // }
    private void SpawnNewEnemy()
    {
        if (m_EnemyPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
        {
            Debug.Log("Spawn points length: " + m_spawnPoints.Length);
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Debug.Log("Random index: " + randomIndex);
            
          //  EnemyObjectPool.SharedInstance.CreateEnemyAtFirstStart();
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
   
   */

//
// public class EnemyManager : MonoBehaviour
// {
//     public Transform[] spawnPoints;
//     public WaveManagerConfig waveManagerConfig;
//
//     private int currentWaveIndex = 0;
//     private bool waveInProgress = false;
//     private List<GameObject> activeEnemies = new List<GameObject>();
//
//     void Start()
//     {
//         StartCoroutine(ManageWaves());
//         GameManager.Instance.player.PlayerDied += OnPlayerDied;
//     }
//
//     private void OnPlayerDied()
//     {
//         waveInProgress = false;
//         StopAllCoroutines();
//     }
//
//     private IEnumerator ManageWaves()
//     {
//         while (currentWaveIndex < waveManagerConfig.waves.Length)
//         {
//             WaveConfig currentWave = waveManagerConfig.waves[currentWaveIndex];
//             waveInProgress = true;
//             yield return StartCoroutine(SpawnWave(currentWave));
//
//             if (currentWave.spawnAfterAllDead)
//             {
//                 yield return new WaitUntil(() => activeEnemies.Count == 0);
//             }
//
//             yield return new WaitForSeconds(currentWave.timeBetweenWaves);
//             waveInProgress = false;
//             currentWaveIndex++;
//         }
//     }
//
//     private IEnumerator SpawnWave(WaveConfig waveConfig)
//     {
//         Debug.Log("current wave" + (currentWaveIndex + 1));
//         foreach (var enemyConfig in waveConfig.enemies)
//         {
//             for (int i = 0; i < enemyConfig.enemyCount; i++)
//             {
//                 SpawnNewEnemy(enemyConfig.enemyPrefab, enemyConfig.healthMultiplier,  enemyConfig.speedMultiplier);
//                 yield return new WaitForSeconds(waveConfig.spawnInterval);
//             }
//
//            
//         }
//     }
//
//     private void SpawnNewEnemy(GameObject enemyPrefab, float healthMultiplier, float speedMultiplier)
//     {
//         if (enemyPrefab != null && spawnPoints.Length > 0)
//         {
//             int randomIndex = Random.Range(0, spawnPoints.Length);
//             Transform spawnPoint = spawnPoints[randomIndex];
//             var enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
//             activeEnemies.Add(enemy);
//            
//                 EnemyAi enemyAiComponent = enemy.GetComponent<EnemyAi>();
//                 if (enemyAiComponent != null)
//                 {
//                     enemyAiComponent.ResetEnemy();
//                     enemyAiComponent._health = Mathf.RoundToInt(enemyAiComponent._health * healthMultiplier);
//                 }
//                 NavMeshAgent agent = enemyAiComponent.GetComponent<NavMeshAgent>();
//                 if (agent != null)
//                 {
//                     agent.speed *= speedMultiplier;
//                 }
//                 
//             //   enemy.GetComponent<EnemyHealth>().OnDeath += () => activeEnemies.Remove(enemy);
//         }
//     }
//     
// }

//TOTAL CLUSTERF*CK
public class EnemyManager : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public WaveManagerConfig waveManagerConfig;
    
    public EnemyBossAi m_BossPrefab;
    private int currentWaveIndex = 0;
    private bool waveInProgress = false;
    private List<GameObject> activeEnemies = new List<GameObject>();
    
    [SerializeField] private  int bossCount;
    private int createdEnemies;
    private int diedEnemies;
    private int diedBossCount;
    
    public  static event Action OnSuccess;
    private void Start()
    {
        EnemyObjectPool.SharedInstance.CreateEnemyAtFirstStart();
        StartCoroutine(ManageWaves());
        GameManager.Instance.player.PlayerDied += OnPlayerDied;
        EnemyAi.OnEnemyDeath += OnEnemyKilled;
        EnemyBossAi.OnEnemyBossDeath += OnBossKilled;
    }

    private void OnBossKilled()
    {
        diedBossCount++;
        if (diedBossCount==bossCount)
        {
            OnSuccess?.Invoke();
        }
    }

    private void OnDestroy()
    {
        EnemyAi.OnEnemyDeath -= OnEnemyKilled;
        EnemyBossAi.OnEnemyBossDeath -= OnBossKilled;
        //belki patlar kim bilir
        GameManager.Instance.player.PlayerDied -= OnPlayerDied;

    }

    private void OnEnemyKilled()
    {
        diedEnemies++;
        if ( createdEnemies == diedEnemies)
        {
            SpawnLoop();
        }
    }

   

    private void SpawnLoop()
    {
        for (int i = 0; i < bossCount; i++)
        {
          
            BossSpawnHandler();

        }
       
    }

    private void BossSpawnHandler()
    {
        Debug.Log("CALL the BOSS");
        //TODO spawn boss
        if (m_BossPrefab != null && m_spawnPoints != null && m_spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Transform spawnPoint = m_spawnPoints[randomIndex % m_spawnPoints.Length];
            var bossAi = Instantiate(m_BossPrefab, spawnPoint.position, spawnPoint.rotation);
            
        }
        else
        {
            Debug.Log("Spawn points array is null or empty.");
        }
        
        
    }

    private void OnPlayerDied()
    {
        waveInProgress = false;
        StopAllCoroutines();
    }

    private IEnumerator ManageWaves()
    {
        
        while (currentWaveIndex < waveManagerConfig.waves.Length)
        {
            WaveConfig currentWave = waveManagerConfig.waves[currentWaveIndex];
            waveInProgress = true;
            yield return StartCoroutine(SpawnWave(currentWave));

            if (currentWave.spawnAfterAllDead)
            {
                yield return new WaitUntil(() => activeEnemies.Count == 0);
            }

           

            yield return new WaitForSeconds(currentWave.timeBetweenWaves);
            waveInProgress = false;
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave(WaveConfig waveConfig)
    {
        Debug.Log("Current wave: " + (currentWaveIndex + 1));
       
        foreach (var enemyConfig in waveConfig.enemies)
        {
            for (int i = 0; i < enemyConfig.enemyCount; i++)
            {
                SpawnNewEnemy(enemyConfig.enemyPrefab, enemyConfig.healthMultiplier, enemyConfig.speedMultiplier);
                createdEnemies++;
                yield return new WaitForSeconds(waveConfig.spawnInterval);
            }
        }
        Debug.Log("enemies count"+ createdEnemies);
    }

    private void SpawnNewEnemy(GameObject enemyPrefab, float healthMultiplier, float speedMultiplier)
    {
        if (enemyPrefab.name == "RagDolledGuyNew Variant")
        {
            if (enemyPrefab != null && m_spawnPoints.Length > 0)
            {
                int randomIndex = Random.Range(0, m_spawnPoints.Length);
                Transform spawnPoint = m_spawnPoints[randomIndex];
                var enemy = EnemyObjectPool.SharedInstance.GetPooledObject();

                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.transform.rotation = spawnPoint.rotation;
                    enemy.SetActive(true);
                    activeEnemies.Add(enemy);

                    EnemyAi enemyAiComponent = enemy.GetComponent<EnemyAi>();
                    if (enemyAiComponent != null)
                    {
                        enemyAiComponent.ResetEnemy();
                        enemyAiComponent._health = Mathf.RoundToInt(enemyAiComponent._health * healthMultiplier);
                    }
                    NavMeshAgent agent = enemyAiComponent.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.speed *= speedMultiplier;
                    }

                }
            }
        }
        //Could be more enemies ...
       
        
       
    }
}