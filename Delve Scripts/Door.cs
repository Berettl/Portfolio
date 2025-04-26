using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // These arrays are used for spawn points and enemy spawn triggers
    [SerializeField] private Transform[] roomSpawnPoints;
    [SerializeField] private Collider[] enemyTriggerColliders;
    [SerializeField] private EnemySpawnerTrigger[] enemySpawnTriggers;

    // Main spawn point for the player when returning
    [SerializeField] private Transform mainSpawnPoint;

    // Flag to check if the prop is the main one
    [SerializeField] private bool isMain = false;

    // Flag to check if the player is in range of the door
    private bool inRange = false;

    // Reference to the player object
    private GameObject player;

    // Reference to the EnemySpawnerTrigger
    [SerializeField] private EnemySpawnerTrigger enemySpawnerTrigger;

    // Update is called once per frame
    void Update()
    {
        // If it's the main door, player is in range, and the room is cleared, allow spawning into a new room
        if (isMain && inRange)
        {
            // Disable further interactions if the room is cleared
            if (Input.GetKeyDown(KeyCode.E))
            {
                SpawnInRoom();
                this.enabled = false;
            }
        }
        // If it's not the main door and the player is in range, return to the main spawn point
        else if (!isMain && inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReturnToMainDoor();
            }
        }
    }

    // When the player enters the trigger area (door)
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.gameObject;
            if (!inRange)
            {
                inRange = true;
            }
        }
    }

    // When the player leaves the trigger area (door)
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (inRange)
            {
                inRange = false;
            }
        }
    }

    // This method spawns the player in a random room
    private void SpawnInRoom()
    {

        int i = Random.Range(0, roomSpawnPoints.Length);
        if (player != null)
        {
            player.transform.position = roomSpawnPoints[i].position;

            // Disable all enemy triggers and spawn points except the one for the current room
            for (int j = 0; j < enemyTriggerColliders.Length; j++)
            {
                if (j == i)
                {
                    enemyTriggerColliders[j].enabled = true;
                    enemySpawnTriggers[j].enabled = true;

                    // Reset altar flag for the new room
                    enemySpawnTriggers[j].ResetAltarResetFlag();
                }
                else
                {
                    enemyTriggerColliders[j].enabled = false;
                    enemySpawnTriggers[j].enabled = false;
                }
            }
        }
    }

    // This method returns the player to the main spawn point
    private void ReturnToMainDoor()
    {
        if (player != null)
        {
            player.transform.position = mainSpawnPoint.position;
        }
    }
}