using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 6;
    private CharacterController charController;
    private float verticalSpeed;
    public float gravity = -9.8f;
    public float jumpForce = 10.0f; // Adjust the jump force as needed
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical inputs
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        // Calculate movement in X and Z directions
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        // Check if the character is grounded
        if (charController.isGrounded)
        {
            // Reset vertical speed if on the ground
            verticalSpeed = -0.5f;

            // Jump input
            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpForce;
            }
        }
        else
        {
            // Apply gravity if not on the ground
            verticalSpeed += gravity * Time.deltaTime;
        }

        // Include vertical movement (jumping/falling)
        movement.y = verticalSpeed;

        // Scale movement by deltaTime and transform direction
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        // Apply movement to the CharacterController
        charController.Move(movement);
    }
}
