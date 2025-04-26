using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAggroRadius : MonoBehaviour
{
    public float aggroRadius = 10f;  // Radius within which enemies can detect the player
    public LayerMask enemyLayer;     // Layer for detecting enemies

    void Update()
    {
        // Check if the player has fired
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Player fired!");
            DetectEnemiesInRange();
        }
    }

    void DetectEnemiesInRange()
    {
        // Find all colliders within the aggro radius on the enemy layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, aggroRadius, enemyLayer);

        Debug.Log($"Detected {colliders.Length} enemies within aggro radius.");

        foreach (Collider collider in colliders)
        {
            // Trigger aggro state on each enemy within range
            AggroEnemy(collider.transform);
        }
    }

    void AggroEnemy(Transform enemyTransform)
    {
        // Assuming the enemy has a script with a method called "SetAggro"
        EnemyTest1 enemyAI = enemyTransform.GetComponent<EnemyTest1>();
        if (enemyAI != null)
        {
            Debug.Log("Aggroing enemy: " + enemyTransform.name);
            enemyAI.SetAggro();
        }
    }

    // For visualization in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
