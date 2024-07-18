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
    void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetBool("idle",false);
        animator.SetBool("run", true);
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
        OnEnemyDeath?.Invoke();
        player_Target.PlayerDied -= OnPlayerDied;

        gameObject.tag = "DiedEnemy";
        int noCollisonLayer = 8;
        agent.enabled = false;
        // gameObject.layer = noCollisonLayer;
        SetLayerRecursively(gameObject, noCollisonLayer);
        
        isDead = true;
        animator.enabled = false;
        EnableRagdoll();
        
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<EnemyAi>().enabled = false;
        
        
        //Destroy(gameObject);
        
        StartCoroutine(DestroyAfterDelay(10f));
    }
    
    private void EnableRagdoll()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
    }
    private void DisableRagdoll()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        // Destroy(gameObject);
            
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
            //todo animation idle
            animator.SetBool("run",false); 
            animator.SetBool("idle",true);
        }
    }

    public void ResetEnemy()
    {
        isDead = false;
        gameObject.tag = "Enemy";
        SetLayerRecursively(gameObject, 0);
        CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.enabled = true;
        }
        else
        {
            Debug.LogError("CapsuleCollider  missing");
        }

        EnemyAi enemyAiComponent = gameObject.GetComponent<EnemyAi>();
        if (enemyAiComponent != null)
        {
            enemyAiComponent.enabled = true;
        }
        else
        {
            Debug.LogError("EnemyAi component is missing");
        }

        if (animator != null)
        {
            animator.enabled = true;
        }
        else
        {
            Debug.LogError("Animator is missing");
        }

        agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = false;
        }
        else
        {
            Debug.LogError("NavMeshAgent missing");
        }

        if (player_Target != null)
        {
            player = player_Target.transform;
            player_Target.PlayerDied += OnPlayerDied;
        }
        else
        {
            Debug.Log("Player target is missing.");
        }
        DisableRagdoll();
        animator.SetBool("run", true);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            if (child == null)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
