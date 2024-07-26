using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GemSpawner : MonoBehaviour
{
    
    public GameObject[] gems; 
    public float range = 10.0f;
    public float spawnInterval = 2.0f; 
    private float spawnTimer;

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    void Update()
    {
      
       
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                
                SpawnGem(point);
            }
           
        }
    }

    void SpawnGem(Vector3 position)
    {
        if (gems.Length > 0)
        {
            int randomIndex = Random.Range(0, gems.Length);
            GameObject gem = Instantiate(gems[randomIndex], position, Quaternion.identity);
            gem.name = gems[randomIndex].name; // Naming the gem for identification
            Debug.Log("Gem spawned: " + gem.name);
        }
        else
        {
            Debug.LogWarning("cant spawn the gems");
        }
    }
}
