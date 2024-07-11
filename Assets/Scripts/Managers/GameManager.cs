using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public EnemyAi enemyAi;
    public Player player;
    public Score score;
    public PlayerHealthController health;
    public EnemyObjectPool enemyPool;
    public SceneHandler sceneControl;
    public DetectTarget detectTarget;
    
    //test
   [SerializeField] public SceneHandler scene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

  
    
}
