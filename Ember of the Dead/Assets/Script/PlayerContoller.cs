using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContoller : MonoBehaviour
{
    //private Alteruna.Avatar _avatar;
    public float moveSpeed = 5f; // Speed of the player movement

    void Start()
    {
        //_avatar = GetComponent<Alteruna.Avatar>();
        /*
        if (!_avatar.IsMe)
            return;
        */
    }

    void Update()
    {
                /*
        if (!_avatar.IsMe)
            return;
        */
        // Player movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        // Apply movement
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Set the z coordinate to 0 since this is a 2D game

        // Calculate direction from player to mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle between the direction and the x-axis (in radians, convert to degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - -90f;

        // Rotate the player to face the mouse position around the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the required tag
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
        }
    }
}
