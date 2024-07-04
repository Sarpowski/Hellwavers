using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action killedAnEnemy;
    public float speed = 10f;
    public float lifetime = 5f; // Projectile will be destroyed after this time

    private Transform target;
    private float timeAlive;

    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else if (timeAlive < lifetime)
        {
            // Move forward if no target (optional, depending on your game design)
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit something: " + other.gameObject.name);
        if (other.CompareTag("Enemy") && other.gameObject.TryGetComponent<EnemyAi>(out var enemyAi))
        {
            enemyAi.CollidedWithProjectile();
            // Add your logic here for what happens when the projectile hits the enemy
            Debug.Log("Projectile hit an enemy: " + other.gameObject.name);
            //killedAnEnemy?.Invoke(); //fixed
            Destroy(gameObject); // Destroy the projectile
        }
    }
   
}

