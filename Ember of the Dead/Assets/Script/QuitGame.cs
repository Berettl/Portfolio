
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    public void Quit()
    {
        // Closes the application
        Debug.Log("Player Quit");

        Application.Quit();
    }
}
