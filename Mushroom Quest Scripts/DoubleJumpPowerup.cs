using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoubleJumpPowerup : MonoBehaviour
{
    // Customize this script as needed.
    public float doubleJumpHeight = 10f;
    public GameObject alternatePlayerPrefab; // Reference to the alternate player prefab
    public Animator doubleJumpAnim;

    private bool hasPowerUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collects the powerup.
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null && !hasPowerUp)
        {
            // Trigger the powerup effect and destroy the powerup object.
            ApplyPowerupEffect(player);
            Destroy(gameObject);
        }
    }

    // Implement the powerup effect here.
    private void ApplyPowerupEffect(PlayerCharacter player)
    {
        // Deactivate the original player
        player.gameObject.SetActive(false);

        // Swap out the player prefab
        GameObject newPlayer = Instantiate(alternatePlayerPrefab, player.transform.position, player.transform.rotation);

        // Get the PlayerCharacter component from the new player
        PlayerCharacter newPlayerCharacter = newPlayer.GetComponent<PlayerCharacter>();

        // Apply the double jump powerup to the new player
        newPlayerCharacter.PickUpDoubleJumpPowerup();

        // Activate the new player
        newPlayer.SetActive(true);
        SetupCameraForNewPlayer(newPlayer);

        newPlayer.GetComponent<Damageable>().SetHealth(player.GetComponent<Damageable>().CurrentHealth);
        // You can add visual/audio effects or update UI to indicate the powerup collection.

        hasPowerUp = true; // Prevent the powerup from being applied multiple times
    }

    private void SetupCameraForNewPlayer(GameObject newPlayer)
    {
        // Find the CinemachineVirtualCamera in the scene
        Cinemachine.CinemachineVirtualCamera virtualCamera =
            FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

        if (virtualCamera != null)
        {
            // Set the camera follow target to the new player
            virtualCamera.Follow = newPlayer.transform;
        }
        else
        {
            Debug.LogWarning("CinemachineVirtualCamera not found in the scene.");
        }
    }
}
