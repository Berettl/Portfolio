using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;  // Reference to the NavMeshAgent component for movement.
    public Transform player;    // Reference to the player's transform for tracking.

    public LayerMask whatIsGround, whatIsPlayer;  // Layer masks for ground and player detection.

    // Patroling
    public Vector3 walkPoint;       // Random point for patrolling.
    bool walkPointSet;              // Flag to check if a walk point is set.
    public float walkPointRange;    // Range within which walk points are set.

    //States
    public float sightRange;        // Range within which the enemy can see the player.
    public bool PlayerInSightRange; // Flag to check if player is within sight range.

    private void Awake()
    {
        // Find and assign the player's transform and get the NavMeshAgent component.
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check if the player is within sight range.
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        // If the player is not in sight range, patrol.
        if (!PlayerInSightRange) Patroling();
        // If the player is not in sight range, chase the player.
        if (!PlayerInSightRange) ChasePlayer();
    }

    private void Patroling()
    {
        // If a walk point is not set, find a new one.
        if (!walkPointSet) SearchWalkPoint();

        // Move towards the walk point if it is set.
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Calculate the distance to the walk point.
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If the distance to the walk point is close enough, mark walk point as not set.
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate a random point within the walk point range.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        // Set the new walk point based on the calculated random point.
        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        // Check if the walk point is on the ground using a raycast.
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Set the destination to the player's position to chase them.
        agent.SetDestination(player.position);
    }

    void OnTriggerEnter(Collider other)
    {
        // If the enemy collides with the player, load the "GameOver" scene.
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
