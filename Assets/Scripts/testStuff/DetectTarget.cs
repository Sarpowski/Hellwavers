using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DetectTarget : MonoBehaviour
{
    public string tagToDetect = "Enemy";
    public GameObject[] allEnemies;
    public GameObject closestEnemy;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootInterval = 1f; // Time between shots
    
    public float detectionRadius = 10f;
    
    private float shootTimer;
    //runtime da patliyor
    // void Start() 
    // {
    //     allEnemies = GameObject.FindGameObjectsWithTag(tagToDetect);
    // }
    //runtime da bulamiyor
    void Update()
    {
        if (GameManager.Instance.player.isDead )
        {
            return;
        }
        
        allEnemies = GameObject.FindGameObjectsWithTag(tagToDetect);    
        
        closestEnemy = ClosestEnemy();
    
        if (closestEnemy != null)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootProjectile();
                shootTimer = 0f;
            }
        }
    }
    
    GameObject ClosestEnemy()
    {
        GameObject closestHere = null;
        float leastDistance = Mathf.Infinity;
    
        foreach (var enemy in allEnemies)
        {
            if (enemy == null)
            {
                continue;
            }
            float distanceHere = Vector3.Distance(transform.position, enemy.transform.position);
    
            if (distanceHere < leastDistance && distanceHere <= detectionRadius) //buraya bir sey ekledim
            {
                leastDistance = distanceHere;
                closestHere = enemy;
            }
        }
    
        return closestHere;
    }
    
    void ShootProjectile()
    {
        if (closestEnemy != null && projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            Projectile projScript = projectile.GetComponent<Projectile>();
            
            if (projScript != null)
            {
                projScript.SetTarget(closestEnemy.transform);
            }
        }
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}