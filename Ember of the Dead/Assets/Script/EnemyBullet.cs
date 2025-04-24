using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 30f; // Speed at which the bullet moves
    public int damage = 1; // Damage dealt by the bullet

    private Vector2 direction; // Direction the bullet will move

    // Method to initialize the bullet's direction
    public void Initialize(Vector2 bulletDirection)
    {
        direction = bulletDirection.normalized; // Normalize the direction vector
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            // Handle collision with player (e.g., reduce health)
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destroy the bullet on collision
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall")) // Example for hitting a wall
        {
            // Destroy the bullet upon hitting a wall
            Destroy(gameObject);
        }
    }
    */
}
