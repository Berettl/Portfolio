using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public PlayerMovement plyerScript; // Reference to PlayerMovement script
    private bool isPlayerInRange = false;
    public PlayerData playerData; // Reference to PlayerData ScriptableObject
    public string hubSceneName = "PlayerHub"; // Name of the Hub scene (update to match your actual scene name)
    public string mazeSceneName = "Maze Level"; // Name of the Hub scene (update to match your actual scene name)
    public PlayerKeyManager PlayerKey;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the current scene is the Hub scene
        if (currentSceneName == hubSceneName)
        {
            // Reset stats when leaving the Hub scene
            playerData.ResetStats();
        }

        if(currentSceneName == mazeSceneName)
        {
            if(PlayerKey.keyFragmentCount == 3)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("Need more keys");
            }
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void Respawn()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // If we are respawning to the Hub, reset stats
        if (currentSceneName == hubSceneName)
        {
            playerData.ResetStats(); // Reset stats on respawn unless it's the Hub scene
        }

        SceneManager.LoadScene("PlayerHub"); // Load the first scene (Hub or Start)
        plyerScript.isAlive = true;
    }
}
