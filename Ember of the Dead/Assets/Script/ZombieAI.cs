using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public int Health = 5;

    private float distance;

    // Ranged Enemy Option
    public bool RangedEnemy; // Ranged Option
    public GameObject Bullet; // Bullet prefab
    public Transform BulletSpawn; // The point where bullets spawn
    public int firerate = 1; // Number of shots per second
    private float nextTimeToFire = 0f; // Timer for the next shot
    public float bulletVelocity = 30f; // Speed at which bullets are fired

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized; // Corrected direction calculation

        // Rotate towards player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - -90f;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        // Move towards the player if within chase range
        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // Shoot at the player if the enemy is a ranged type and time to fire has elapsed
        if (RangedEnemy && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate; // Calculate time for the next shot
            ShootProjectile(direction);
        }
    }

    private void ShootProjectile(Vector2 direction)
    {
        // Instantiate the projectile at the bullet spawn position with no rotation
        GameObject projectile = Instantiate(Bullet, BulletSpawn.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set projectile velocity toward the player
            rb.velocity = direction * bulletVelocity;
        }
    }

    public void RemoveHealth(int damage)
    {
        Health -= damage;
        Debug.Log("Enemy Health: " + Health);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
