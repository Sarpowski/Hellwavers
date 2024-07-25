using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Config", menuName = "ScriptableObjects/WaveConfig",order = 1)]
public class WaveConfig : ScriptableObject
{
    public EnemyConfig[] enemies;
    public float spawnInterval;
    public float timeBetweenWaves;
    public bool spawnAfterAllDead;
   // public bool atCurrentWaveBoss;
}
[System.Serializable]
public class EnemyConfig
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float healthMultiplier;
    public float speedMultiplier;
}
