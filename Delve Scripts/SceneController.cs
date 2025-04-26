using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //These variables call for the button audio sources
    [SerializeField] private AudioSource playButton;
    [SerializeField] private AudioSource quitButton;

    private string newSceneName;

    //This method takes a scene name and loads that scene
    public void LoadScene(string sceneName) {
        newSceneName = sceneName;
        StartCoroutine(PlayAudioClip());
    }

    //This method handles quitting the application
    public void QuitScene() {
        StartCoroutine(QuitAudioClip());
    }

    //This method is a player for the play button audio source with a waiter
    private IEnumerator PlayAudioClip() {
        playButton.Play();
        yield return new WaitForSecondsRealtime(2.1f);
        playButton.Stop();
        SceneManager.LoadScene(newSceneName);
    }

    //This method is a player for the quit button audio source with a waiter
    private IEnumerator QuitAudioClip() {
        quitButton.Play();
        yield return new WaitForSecondsRealtime(2f);
        quitButton.Stop();
        Application.Quit();
    }
}
