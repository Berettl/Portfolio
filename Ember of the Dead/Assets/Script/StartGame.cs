
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void TitleScreen()
    {
        // Loads the title screen
        // The buildIndex stores all levels
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
