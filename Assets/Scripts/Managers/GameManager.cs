using System;
using System.Collections;
using System.Collections.Generic;
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
    //test
   [SerializeField] public SceneHandler scene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            //Sor ogren niye
            
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

  
    
}
