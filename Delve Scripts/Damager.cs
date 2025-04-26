using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage = 1;
    public float damageInterval = 0.5f; // Time between damage ticks

    private float nextDamageTime = 0f; // Tracks when to apply damage again

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null && Time.time >= nextDamageTime)
            {
                player.Hurt(damage);
                nextDamageTime = Time.time + damageInterval; // Set next damage time
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f; // Reset damage timer when leaving the laser
        }
    }
}
