using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    //These variables are meant for camera movement
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform[] cameraAnchors;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float camPanMagnitude = 0.05f;

    //This variable is meant for credits control
    [SerializeField] private GameObject credits;

    //These variables call for the button audio sources
    [SerializeField] private AudioSource creditsButton;

    //These booleans handle the camera movement
    private bool panning = false;
    private bool moving = false;

    // Upddate is called once per frame
    void Update()
    {
        //This corouting moves the camera through the different areas of the scene
        if (!moving) {
            StartCoroutine(CameraMovement());
        }

        //This coroutine pans the camera
        if (!panning) {
            StartCoroutine(CameraPan());
        }
    }

    //This waiter method handles the camera rotation and spawn movement
    private IEnumerator CameraMovement() {
        moving = true;
        for (int i = 0; i < cameraAnchors.Length; i++) {
            mainCam.transform.position = cameraAnchors[i].position;
            mainCam.transform.rotation = cameraAnchors[i].rotation;
            yield return new WaitForSeconds(waitTime);
        }
        moving = false;
    }

    //This public method is for toggling the credits
    public void ToggleCredits() {
        StartCoroutine(PlayAudioClip(creditsButton));
    }

    //This waiter method handles the camera panning
    //Ignore the insane amount of lines
    //The more lines, the smoother the movement
    private IEnumerator CameraPan() {
        panning = true;
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(-camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        mainCam.transform.Translate(camPanMagnitude, 0, 0);
        yield return new WaitForSeconds(0.1f);
        panning = false;
    }

    //This method is a player for audio sources with a waiter
    private IEnumerator PlayAudioClip(AudioSource audioSource) {
        audioSource.Play();
        yield return new WaitForSeconds(0.25f);
        if (!credits.activeInHierarchy) {
            credits.SetActive(true);
        }
        else {
            credits.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        audioSource.Stop();
    }
}
