using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public static event Action killedAnEnemy;
    public float speed = 10f;
    public float lifetime = 5f; // Projectile will be destroyed after this time

    private Vector3 direction;
    private float timeAlive;
    private DetectTarget detectTarget;
   
    private void Awake()
    {
        detectTarget = FindObjectOfType<DetectTarget>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            DeactivateAndReturnToPool();
        }

      
        transform.position += direction * (speed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {   Collider targetCollider = target.GetComponent<Collider>(); //not null safety 
        //TODO add null safety/check  for targetCollider 
        if (target != null)
        {
            Vector3 chestPosition = targetCollider.bounds.center;// idk if its best way to handle  
            direction = (chestPosition - transform.position).normalized;
            //direction = (target.position - transform.position).normalized;
        }
        timeAlive = 0f; // Reset the lifetime counter
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit something: " + other.gameObject.name);
        if (other.CompareTag("Enemy") && other.gameObject.TryGetComponent<EnemyAi>(out var enemyAi))
        {
            enemyAi.CollidedWithProjectile();
            Debug.Log("Projectile hit an enemy: " + other.gameObject.name);
            DeactivateAndReturnToPool();
        }

        if (other.CompareTag("Enemy") && other.gameObject.TryGetComponent<EnemyBossAi>(out var enemyBossAi))
        {
            enemyBossAi.CollidedWithProjectile();
            Debug.Log("Projectile hit an enemy: " + other.gameObject.name);
            DeactivateAndReturnToPool();
        }
    }

    private void DeactivateAndReturnToPool()
    {
        detectTarget.ReturnBulletToPool(gameObject);
    }
    
    
}