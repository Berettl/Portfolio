using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    public static bool isGamePaused = false;

    void Update()
    {
        if (!AltarInteraction.isGamePaused) {
            if (isGamePaused == false && Input.GetKeyDown(KeyCode.Escape)) {
                canvas.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                isGamePaused = true;
                Debug.Log(isGamePaused);
            }
        }
    }

    public void TerminateGame()
    {
        Application.Quit();
        Debug.Log("Session terminated.");
    }

    public void CloseMenu()
    {
        canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isGamePaused = false;
    }
}
