using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 1;
    //If enemy enters trigger of the bullet
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damageable health = other.GetComponent<Damageable>();
            //Damageable script does not equal null
            if (health != null)
            {
                //Enemy take Damage
                health.TakeDamage(Damage);
            }
            else
            {
                //Enemy does not have damageable script
                Debug.LogWarning("Damageable component not found on enemy GameObject.");
            }
        }
    }
}
