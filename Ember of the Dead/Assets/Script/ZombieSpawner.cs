using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie; // The Zombie prefab
    public float SpawnRange = 15f; // The radius of the circle
    public float InitialSpawnInterval = 5f; // The initial time between spawns
    public float SpawnRateIncrease = 0.95f; // Percentage decrease in spawn interval per spawn
    public float MinimumSpawnInterval = 0.5f; // Minimum time between spawns

    private float currentSpawnInterval; // The current time between spawns
    private float spawnTimer; // A timer to keep track of when to spawn the next zombie

    void Start()
    {
        currentSpawnInterval = InitialSpawnInterval;
        spawnTimer = currentSpawnInterval;
    }

    void Update()
    {
        // Decrease the timer
        spawnTimer -= Time.deltaTime;

        // Check if it's time to spawn a new zombie
        if (spawnTimer <= 0f)
        {
            SpawnZombie();
            // Reset the spawn timer
            currentSpawnInterval = Mathf.Max(MinimumSpawnInterval, currentSpawnInterval * SpawnRateIncrease);
            spawnTimer = currentSpawnInterval;
        }
    }

    void SpawnZombie()
    {
        // Get a random angle in radians to calculate the spawn position on the circle
        float angle = Random.Range(0f, Mathf.PI * 2); // Full circle (0 to 2? radians)

        // Calculate the spawn position on the circumference of the circle (X-Y plane)
        float xPos = Mathf.Cos(angle) * SpawnRange;
        float yPos = Mathf.Sin(angle) * SpawnRange;
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f) + transform.position; // Z is 0 for 2D game

        // Face the zombie towards the center of the spawn area
        Vector3 directionToCenter = (transform.position - spawnPosition).normalized;

        // Instantiate the zombie and make it face the center
        Instantiate(Zombie, spawnPosition, Quaternion.LookRotation(Vector3.forward, directionToCenter));
    }

    // Visualize the spawn range in the scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpawnRange);
    }
}
