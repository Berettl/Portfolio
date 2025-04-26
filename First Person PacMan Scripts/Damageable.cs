using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    private Vector3 startPosition;

    void Start()
    {
        currentHealth = maxHealth; //Change current health to MaxHealth
        startPosition = transform.position; // Store the starting position
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(RespawnEnemy(0)); // Respawn enemy
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage; //Subtracts damage to current health
        Debug.Log("Take Damage");
    }

    IEnumerator RespawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Reset health and move the enemy to the starting position
        currentHealth = maxHealth;
        transform.position = startPosition;
    }
}
