using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmingPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Hurt(5);
            Debug.Log($"Health: {player.currentHealth}");

            // Instead of destroying, deactivate the object for reuse
            DeactivateProjectile();
        }
    }

    private void DeactivateProjectile()
    {
        // Disable the projectile instead of destroying it
        gameObject.SetActive(false);
    }
}
