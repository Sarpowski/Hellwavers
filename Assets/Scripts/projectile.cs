using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the projectile after a certain time
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic here (e.g., damage to enemy)
        Destroy(gameObject); // Destroy the projectile on collision
    }
}
