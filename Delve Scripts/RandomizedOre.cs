using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedOre : MonoBehaviour
{
    public List<GameObject> ores;
    public LayerMask obstacleLayer; // Layer for walls
    public LayerMask groundLayer;   // Layer for ground/terrain
    public int maxAttempts = 15;    // How many times we try to find a good position
    public float positionAdjustStep = 0.2f; // How much we move per attempt
    public float groundCheckDistance = 10f; // Distance to check for ground
    public float heightOffset = 0.5f; // How much to raise ore to avoid clipping

    void Awake()
    {
        int oreSpawn = Random.Range(0, ores.Count);
        GameObject selectedOre = ores[oreSpawn];

        Vector3 spawnPosition = transform.position;
        Quaternion randomRotation;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Generate a new random rotation
            randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            // Check if the area is free before spawning
            if (!IsColliding(spawnPosition, selectedOre, randomRotation))
            {
                // Adjust position to stay above the ground
                spawnPosition = AdjustToGround(spawnPosition, selectedOre);

                // Final check to ensure it's in a good spot
                if (!IsColliding(spawnPosition, selectedOre, randomRotation))
                {
                    Instantiate(selectedOre, spawnPosition, randomRotation);
                    return; // Successfully spawned
                }
            }

            // Move position slightly and retry
            spawnPosition += Random.insideUnitSphere * positionAdjustStep;
            spawnPosition.y = transform.position.y; // Reset Y to prevent floating ores
        }

        // If we fail, spawn without rotation as a fallback
        Debug.LogWarning("Failed to find a valid spawn position, spawning with no rotation.");
        Vector3 finalPosition = AdjustToGround(transform.position, selectedOre);
        Instantiate(selectedOre, finalPosition, Quaternion.identity);
    }

    // **Check if the spawn position is safe before placing the ore**
    bool IsColliding(Vector3 position, GameObject ore, Quaternion rotation)
    {
        Collider oreCollider = ore.GetComponent<Collider>();

        if (oreCollider == null)
        {
            Debug.LogWarning("Ore prefab is missing a collider. Cannot check collisions.");
            return false; // Assume it's safe to spawn
        }

        Vector3 boundsSize = oreCollider.bounds.extents; // Get half size
        Vector3 center = position + rotation * oreCollider.bounds.center; // Get rotated center

        // Use `Physics.CheckBox` instead of `OverlapBox` to be more precise
        bool isColliding = Physics.CheckBox(center, boundsSize, rotation, obstacleLayer);
        return isColliding;
    }

    // **Ensure the ore stays above the ground**
    Vector3 AdjustToGround(Vector3 position, GameObject ore)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * groundCheckDistance, Vector3.down, out hit, groundCheckDistance * 2, groundLayer))
        {
            float oreHeight = GetColliderHeight(ore);
            position.y = hit.point.y + (oreHeight / 2f) + heightOffset;
        }
        return position;
    }

    // **Get the height of the ore**
    float GetColliderHeight(GameObject ore)
    {
        Collider oreCollider = ore.GetComponent<Collider>();
        return oreCollider != null ? oreCollider.bounds.size.y : 1f; // Default height if no collider
    }
}

