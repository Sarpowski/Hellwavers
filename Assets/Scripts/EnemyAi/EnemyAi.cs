using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    
    public Transform player;
    private NavMeshAgent agent;
    public Player player_Target;

    public static event Action OnEnemyDeath;
    
    [SerializeField] public int _health;
    // Start is called before the first frame update
    void Start()
    {   
        
        agent = GetComponent<NavMeshAgent>();
        
        if (GameManager.Instance != null)
        {
            player_Target = GameManager.Instance.player;
        }

        if (player_Target != null)
        {
            player = player_Target.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.destination = player.position;
        }
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
        Debug.Log("enemyyyy dieeeeee invokeeeee");
        OnEnemyDeath?.Invoke();
        
        Destroy(gameObject);
    }

    public void CollidedWithProjectile()
    {
        Die();
    }
}
