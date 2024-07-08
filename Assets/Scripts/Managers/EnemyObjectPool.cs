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
   }

   private void Start()
   {
      CreateEnemyAtFirstStart();
   }

   private void CreateEnemyAtFirstStart()
   {
      for (int i = 0; i < _amountOfEnemyPrefab; i++)
      {
         _enemyInstantiate = Instantiate(_EnemyPrefab);
         _enemyInstantiate.SetActive(false);
         _pooledEnemy.Add(_enemyInstantiate);
      }
      
   }

   public GameObject GetPooledObject()
   {
      for (int i = 0; i < _amountOfEnemyPrefab; i++)
      {
         if (!_pooledEnemy[i].activeInHierarchy)
         {
            return _pooledEnemy[i];
         }
      }

      return null;
   }
   
}
