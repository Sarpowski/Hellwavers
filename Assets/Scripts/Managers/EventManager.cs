using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    

public class EventManager : MonoBehaviour
{
    
    public  static event Action OnEnemyBoss;

    
    public void CallEnemyBoss()
    {
        Debug.Log("OnEnemyBoss Invoked");
        OnEnemyBoss?.Invoke();
    }
}
