using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //Load level 1
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    //Load level 2
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
    //Load level 3
    public void Level3()
    {
        SceneManager.LoadScene("Level 3"); ;
    }
}
