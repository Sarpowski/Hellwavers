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
    public Animator animator; 
    public static event Action OnEnemyDeath;
    
    public bool isDead { get; private set; } = false;
    
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
            player_Target.PlayerDied += OnPlayerDied;
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
    //enemyStat manager could be 
    private void Die()
    {
        Debug.Log("Enemy DIE from EnemyAI debug");
        OnEnemyDeath?.Invoke();
        player_Target.PlayerDied -= OnPlayerDied;

        gameObject.tag = "DiedEnemy";
        isDead = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<EnemyAi>().enabled = false;
        animator.enabled = false;
        
        //TODO wait 10sec and destroy the object 
        //Destroy(gameObject);
        
        StartCoroutine(DestroyAfterDelay(10f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
            
    }
    public void CollidedWithProjectile()
    {
        Die();
    }
    private void OnPlayerDied()
    {
        if (agent.isOnNavMesh)
        {
            agent.isStopped= true;
        }
    }

}
