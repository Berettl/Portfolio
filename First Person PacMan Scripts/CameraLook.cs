using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLook : MonoBehaviour
{
    // Enum to define the rotation axes
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    // Selected rotation axes
    public RotationAxes axes = RotationAxes.MouseXAndY;

    // Sensitivity for horizontal and vertical rotation
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    // Minimum and maximum vertical rotation angles
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0;

    void Start()
    {
        Cursor.visible = false; // Hide cursor initially

        // Get the player's Rigidbody component
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Check which rotation axes are selected and apply rotation accordingly
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
        else
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }

    // Called when a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the loaded scene and adjust cursor behavior accordingly
        if (scene.name == "Level1")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // Hide cursor in level 1
        }
        else if (scene.name == "GameOver")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // Show cursor in "GameOver" scene
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // Hide cursor for other scenes
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
