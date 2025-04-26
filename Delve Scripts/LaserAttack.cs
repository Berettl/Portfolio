using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public GameObject laserPrefab; // Assign the laser prefab (a thin horizontal object)
    public Transform firePoint; // The starting point of the laser
    public float laserDuration = 0.5f; // How long the laser stays
    public float laserSpeed = 50f; // Speed of stretching
    public float fireRate = 3f; // Time between each laser attack
    public float attackRange = 10f; // The distance at which the enemy starts attacking

    private GameObject currentLaser; // Reference to the spawned laser
    private float fireTimer; // Timer to track when to fire
    private Transform player; // Player reference

    void Start()
    {
        fireTimer = fireRate; // Start firing after the first interval
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find player once at start
    }

    void Update()
    {
        if (player == null) return; // Safety check

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && PauseMenu.isGamePaused == false) // Check if player is within range
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                FireLaser();
                fireTimer = fireRate; // Reset the timer
            }
        }
    }

    void FireLaser()
    {
        if (currentLaser != null) return; // Prevent multiple lasers at once

        currentLaser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        StartCoroutine(ExtendLaser());
    }

    private IEnumerator ExtendLaser()
    {
        if (player == null) yield break; // Safety check

        Vector3 direction = (player.position - firePoint.position).normalized;
        float distance = Vector3.Distance(firePoint.position, player.position);

        // Set rotation to face the player
        currentLaser.transform.rotation = Quaternion.LookRotation(direction);

        float currentLength = 0f;
        Vector3 initialScale = currentLaser.transform.localScale;

        while (currentLength < distance)
        {
            currentLength += laserSpeed * Time.deltaTime;
            currentLaser.transform.localScale = new Vector3(initialScale.x, initialScale.y, currentLength);
            currentLaser.transform.position = firePoint.position + direction * (currentLength / 2); // Move laser forward 
            yield return null;
        }

        yield return new WaitForSeconds(laserDuration);
        Destroy(currentLaser);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
