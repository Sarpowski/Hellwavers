using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Pool;
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
    [SerializeField] private int bulletAmount = 3;
    private Queue<GameObject> _bulletPool = new Queue<GameObject>();
    private GameObject _bulletInstantiate;
  
    private void Start()
    {
        CreateBulletsAtFirst();
    }

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

    private void CreateBulletsAtFirst()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            _bulletInstantiate = Instantiate(projectilePrefab);
            _bulletInstantiate.SetActive(false);
            _bulletPool.Enqueue(_bulletInstantiate);
        }
        
    }

    private GameObject GetBulletFromPool()
    {
        foreach (GameObject bullet in _bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
            
        }

        return null;
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
            //TODO ObjectPool
            // GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
             
            _bulletInstantiate = GetBulletFromPool();
            if (_bulletInstantiate != null)
            {
                _bulletInstantiate.transform.position = shootPoint.transform.position;
                _bulletInstantiate.SetActive(true);
              //  StartCoroutine(FalseBulletGameObject(_bulletInstantiate));
            }
            Projectile projScript = _bulletInstantiate.GetComponent<Projectile>();
            
            if (projScript != null)
            {
                projScript.SetTarget(closestEnemy.transform);
            }
        }
    }

  
    IEnumerator FalseBulletGameObject(GameObject bullet)
    {
        yield return new WaitForSeconds(0.8f);
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position to visualize the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
    
}
