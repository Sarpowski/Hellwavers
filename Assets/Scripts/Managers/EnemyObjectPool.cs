using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
   public static EnemyObjectPool SharedInstance;
   [SerializeField] private GameObject _EnemyPrefab;
   [SerializeField] private int _amountOfEnemyPrefab;

   private GameObject _enemyInstantiate;
   private List<GameObject> _pooledEnemy = new List<GameObject>();

   private void Awake()
   {
      SharedInstance = this;
      Debug.Log("EnemyObjectPool instance created.");
   }

   private void Start()
   {
      // Debug.Log("Start method called.");
      // Debug.Log($"_amountOfEnemyPrefab: {_amountOfEnemyPrefab}");
      // CreateEnemyAtFirstStart();
   }

   public void CreateEnemyAtFirstStart()
   {
      if (_amountOfEnemyPrefab <= 0)
      {
         Debug.LogError("Amount of Enemy Prefabs must be greater than 0.");
         return;
      }
      for (int i = 0; i < _amountOfEnemyPrefab; i++)
      {
         _enemyInstantiate = Instantiate(_EnemyPrefab);
         _enemyInstantiate.SetActive(false);
         _pooledEnemy.Add(_enemyInstantiate);
      }
      Debug.Log($"Created {_amountOfEnemyPrefab} enemy instances.");
     // PrintPoolState();
   }

   public GameObject GetPooledObject()
   {
      if (_pooledEnemy == null)
      {
         Debug.LogError("Pooled enemy list is null.");
         return null;
      }
        
      if (_pooledEnemy.Count == 0)
      {
         Debug.LogError("Pooled enemy list is empty.");
         return null;
      }
      
      for (int i = 0; i < _amountOfEnemyPrefab; i++)
      {
         if (!_pooledEnemy[i].activeInHierarchy)
         {
           
            return _pooledEnemy[i];
         }
      }
      
      return null;
   }
   private void PrintPoolState()
   {
      Debug.Log($"PooledEnemy List Count: {_pooledEnemy.Count}");
      for (int i = 0; i < _pooledEnemy.Count; i++)
      {
         Debug.Log($"Enemy {i}: Active = {_pooledEnemy[i].activeInHierarchy}");
      }
   }
   
}
