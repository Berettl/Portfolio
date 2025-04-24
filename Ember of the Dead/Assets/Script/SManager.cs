
using UnityEngine;

// I used this so I could manage different scenes
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    // I made this public in case I want to change the the delay in Unity's inspector
    // Floats can store decimal numbers in them  
    public float RestartDelay = .1f;


    public GameObject WonLevel;


    // Bool stores a value that is either true or false
    bool EndOfGame = false;


    // Using Debug.Log tells the console that I won the level
    // SetActive will make the level that I chose go to the next scene depending on if I make it true or false
    public void LevelComplete()
    {
        // Debug.Log will tell the console anything in those parenthesis
        Debug.Log("LEVEL WON");

        WonLevel.SetActive(true);

    }  


    // I made GameOver public so I could put this script into the Player Collision script
    public void GameOver()
    {

        // This is so that when the game ends it only ends once
        if (EndOfGame == false)
        {
            EndOfGame = true;

            // Debug.Log will tell the council anything in those parenthesis
            Debug.Log("GAME OVER");

            // Invoke will delay the time it takes to restart the game
            Invoke("RestartGame", RestartDelay);
        }
         
    }

    //So the player could restart from their spawn point
    void RestartGame()
    {
        //When the game restarts it will load the level the player is currently on
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
