using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DetectClosest : MonoBehaviour  //maybe shoot too who knows 
{
    public string tagToDetect = "Enemy";
    public GameObject[] allEnemies;
    public GameObject closestEnemy;
  
    //
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform projectileSpawnPoint; // Reference to the spawn point of the projectile
    public float shootingInterval = 1f; // Time between shots
    private float lastShotTime;
    
    void Start()
    {
        allEnemies = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = ClosestEnemy();
        Debug.Log(closestEnemy.name);
        
        if (Time.time - lastShotTime > shootingInterval)
        {
            ShootProjectile();
            lastShotTime = Time.time;
        }
    }

    GameObject ClosestEnemy()
    {
        GameObject closestHere = gameObject;
        float leastDistance = 5f;//Mathf.Infinity;

        foreach (var enemy in allEnemies)
        {
            if (enemy == null)
            {
                continue;
            }
            float distanceHere = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceHere < leastDistance)
            {
                leastDistance = distanceHere;
                closestHere = enemy;
            }
        }
        
        return closestHere;
        
    }
    
    void ShootProjectile()
    {
        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.transform.position - projectileSpawnPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, UnityEngine.Quaternion.LookRotation(direction));
            projectile.GetComponent<Rigidbody>().velocity = direction * projectile.GetComponent<projectile>().speed;
            
           
        }
    }
}
