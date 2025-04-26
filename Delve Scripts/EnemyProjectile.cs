using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author: Rebekah Bledsoe
 * 
 * This script was modeled after the hurty block script to work
 * with the enemy's projectiles.
 * 
 * Worked with Xiong and his ranged enemy script to develop this one.
 * The enemy projectiles now should damage the player when they hit the
 * player.
 **/

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int projectileDamage = 15;

    //When the projectile sphere enters the player trigger, it will
    //remove health from the player. (You can specify the damage in the
    //inspector window of the Sphere asset)
    private void OnTriggerEnter(Collider other) {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null) {
            player.Hurt(projectileDamage);
        }
    }
}
