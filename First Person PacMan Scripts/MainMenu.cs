using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PLay()
    {
        //Loads first level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Level()
    {
        //Loads level selector level
        SceneManager.LoadScene("LevelSelector");
    }
    public void Quit()
    {
        //Quit the game
        Application.Quit();
        Debug.Log("Quit");
    }
}
