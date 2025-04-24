using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the z coordinate to 0 since this is a 2D game
        mousePosition.z = 0f;

        // Calculate direction from gun to mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle between the direction and the x-axis (in radians, convert to degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the gun around the Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
