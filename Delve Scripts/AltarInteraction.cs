using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteraction : MonoBehaviour
{
    public GameObject[] canvas; // Array of canvases to choose from
    private bool inInteractionZone = false; // Whether the player is in the interaction zone
    public static bool isGamePaused = false; // Whether the game is paused
    private bool hasInteracted = false; // Whether the altar has been interacted with in the current room

    private GameObject currentAltar; // The currently active altar canvas

    void Update()
    {
        if (!PauseMenu.isGamePaused) {
            // Check if the player is in the interaction zone and presses the 'E' key
            if (inInteractionZone && Input.GetKeyDown(KeyCode.E) && hasInteracted == false) {
                InteractWithAltar();
            }

            // Check if the player presses the 'Tab' key to close the altar
            if (Input.GetKeyDown(KeyCode.Tab)) {
                CloseAltar();
            }
        }
    }

    void InteractWithAltar()
    {
        // Ensure the canvas array is not empty
        if (canvas != null && canvas.Length > 0)
        {
            // Pick a new random canvas from the array
            int randomIndex = Random.Range(0, canvas.Length);
            currentAltar = canvas[randomIndex];

            // Activate the new canvas
            currentAltar.SetActive(true);

            // Unlock and show the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause the game
            isGamePaused = true;

            // Mark the altar as interacted with
            hasInteracted = true;
        }
    }

    void CloseAltar()
    {
        // Ensure there is an active altar
        if (currentAltar != null)
        {
            // Deactivate the current altar
            currentAltar.SetActive(false);

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Unpause the game
            isGamePaused = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the interaction zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered Altar interaction zone.");
            inInteractionZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the interaction zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Left Altar Interaction Zone");
            inInteractionZone = false;
        }
    }

    // Call this method when entering a new room to reset the interaction state
    public void ResetInteractionState()
    {
        hasInteracted = false;
        Debug.Log("Altar interaction state reset for the new room.");
    }
}
