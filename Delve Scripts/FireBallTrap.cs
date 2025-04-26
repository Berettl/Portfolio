using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrap : MonoBehaviour
{
    public GameObject FireBall;
    public Transform spawnPoint;
    public float fireballSpeed = 10f;
    public int fireballCount = 3;  // Number of fireballs to spawn
    public float angleSpread = 30f; // Spread angle in degrees
    public float fireballInterval = 2f; // Time between fireball shots
    private bool isPlayerInside = false; // Tracks if the player is in the trigger zone

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            StartCoroutine(FireballLoop());
        }
    }

    IEnumerator FireballLoop()
    {
        while (isPlayerInside)
        {
            SpawnFireball();
            yield return new WaitForSeconds(fireballInterval); // Wait before shooting again
        }
    }

    void SpawnFireball()
    {
        for (int i = 0; i < fireballCount; i++)
        {
            float angleOffset = ((float)i / (fireballCount - 1) - 0.5f) * angleSpread;
            Quaternion fireballRotation = Quaternion.Euler(0, angleOffset, 0) * spawnPoint.rotation;

            GameObject fireballInstance = Instantiate(FireBall, spawnPoint.position, fireballRotation);
            Rigidbody rb = fireballInstance.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = fireballRotation * Vector3.forward * fireballSpeed;
            }
        }
    }
}
