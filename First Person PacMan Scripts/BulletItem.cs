using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    public int Bullets = 5;
    //if player enters ammo items trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Get the gun script component
            Gun ChangeAmmo = other.transform.root.GetComponentInChildren<Gun>();
            //Adds ammo to Gun script
            ChangeAmmo.AddAmmo(Bullets);
            //Destroy Ammo item
            Destroy(gameObject);

        }
    }
}
