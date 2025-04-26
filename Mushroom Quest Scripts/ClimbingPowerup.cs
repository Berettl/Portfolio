using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamekit2D
{
    public class ClimbingPowerup : MonoBehaviour
    {
        public LayerMask Ladder;
        private bool hasPowerUp = false;
        public bool canClimbLadder = false;
        public GameObject alternatePlayer; // Reference to the alternate player prefab

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !hasPowerUp)
            {
                hasPowerUp = true;
                // Disable the ladder power-up object when collected
                gameObject.SetActive(false);

                // Deactivate the original player
                other.gameObject.SetActive(false);

                // Instantiate a new player prefab
                GameObject newPlayer = Instantiate(alternatePlayer, other.transform.position, other.transform.rotation);

                // Get the PlayerCharacter component from the new player
                PlayerCharacter newPlayerCharacter = newPlayer.GetComponent<PlayerCharacter>();

                // You may need to add a method to the PlayerCharacter script to enable climbing
                // Example: newPlayerCharacter.EnableClimbing();
                canClimbLadder = true;

                // Activate the new player
                newPlayer.SetActive(true);
                newPlayer.GetComponent<CharacterController2D>().ladderClimbingAbility = this;
                SetupCameraForNewPlayer(newPlayer);

                // Change the tag of the ladder object to "Ladder"
                GameObject ladderObject = GameObject.Find("Ladder"); // Replace "Ladder" with the actual ladder object name
                if (ladderObject != null)
                {
                    ladderObject.tag = "Ladder";
                }
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
}