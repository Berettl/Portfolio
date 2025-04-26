using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerup : MonoBehaviour
{
    public int damageIncrease = 10;
    public GameObject alternatePlayer; // Reference to the alternate player prefab





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate the original player
            other.gameObject.SetActive(false);

            // Instantiate a new player prefab
            GameObject newPlayer = Instantiate(alternatePlayer, other.transform.position, other.transform.rotation);

            // Get the PlayerCharacter component from the new player
            Damager newPlayerDamageHandler = newPlayer.GetComponent<Damager>();

            // Activate the new player
            newPlayer.SetActive(true);

            if (newPlayerDamageHandler != null)
            {
                // Apply the damage increase to the new player
                newPlayerDamageHandler.IncreaseDamage(damageIncrease);
            }
            SetupCameraForNewPlayer(newPlayer);


            // Destroy the power-up object
            Destroy(gameObject);
            //newPlayer.GetComponent<Damageable>().SetHealth(player.GetComponent<Damageable>().CurrentHealth);

        }
          
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
