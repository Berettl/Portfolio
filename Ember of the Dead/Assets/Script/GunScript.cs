using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform BulletSpawn; // The point where bullets spawn
    public GameObject Bullet; // The bullet prefab
    public bool Shotgun; // Determines if the gun is a shotgun
    public int firerate; // How many shots per second
    private float nextTimeToFire = 0f; // Timer for the next shot
    public int pelletCount = 8; // Number of pellets for shotgun mode
    public float spreadAngle = 30f; // Spread angle for shotgun mode
    public float bulletVelocity = 30f; // Speed at which bullets are fired

    void Update()
    {
        // Fire based on user input and fire rate
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Shotgun)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                // Generate random angles for spread
                float randomSpread = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

                // Calculate the rotation for the pellet (only on Z-axis for 2D)
                Quaternion pelletRotation = BulletSpawn.rotation * Quaternion.Euler(0, 0, randomSpread);

                // Instantiate the pellet
                GameObject pellet = Instantiate(Bullet, BulletSpawn.position, pelletRotation);

                // Get the Rigidbody2D component and apply force in the forward direction
                Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = pelletRotation * Vector2.right * bulletVelocity; // Use Vector2.right for 2D
                }
            }
        }
        else
        {
            // Single bullet fire for non-shotgun
            GameObject projectile = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = BulletSpawn.right * bulletVelocity; // Use Vector2.right for 2D
            }
        }
    }
}
