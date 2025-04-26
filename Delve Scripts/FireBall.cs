using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int Damage = 1;
    public float destroyTime = 5f; // Time before the fireball gets destroyed

    void Start()
    {
        Destroy(gameObject, destroyTime); // Automatically destroy the fireball after X seconds
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                Debug.Log("Player found! Applying damage.");
                player.Hurt(Damage);
            }

            Destroy(gameObject); // Destroy the fireball on impact
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
