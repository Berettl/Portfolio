using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;
    public int Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;

        Destroy(gameObject, 2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            ZombieAI health = other.GetComponent<ZombieAI>();
            // Check if the enemy has the ZombieAI component
            if (health != null)
            {
                // Enemy takes damage
                health.RemoveHealth(Damage);
            }
        }
    }
}
