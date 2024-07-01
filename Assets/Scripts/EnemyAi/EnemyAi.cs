using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    
    public Transform player;
    private NavMeshAgent agent;
    

    public static event Action OnEnemyDeath;
    
    [SerializeField] public int _health;
    // Start is called before the first frame update
    void Start()
    {   
        
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("enemy got hitted by player");
            Die();

        }
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke();
        
        Destroy(gameObject);
    }
}
