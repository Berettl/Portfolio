using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // Method to restart the game
    public void RestartGame()
    {
        // Loads the first scene (make sure your first scene is named "GameScene" or change it as needed)
        SceneManager.LoadScene("MainScene");
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Logs a message for testing
        Debug.Log("Quitting game...");

        // Quits the application
        Application.Quit();

        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
