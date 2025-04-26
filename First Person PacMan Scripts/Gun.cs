using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public int Bullets = 0;
    public TMP_Text Ammo_UI;
    public AudioSource GunShot;

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoUI();
        if (Bullets > 0)
        {
            //If left click then fire bullet
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireTime)
            {
                FireWeapon();
                nextFireTime = Time.time + 1f / fireRate; // Update the next allowed fire time
            }
        }
    }
    //Fires the gun
    private void FireWeapon()
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(Bullet, bulletSpawn.position, Quaternion.identity);
        // Shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        GunShot.Play();
        // Destroy the bullet 
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        Bullets--;
    }
    //Destroy Bullets after a bit after shot
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    //Updates Ammo UI if Ammo picked up
    private void UpdateAmmoUI()
    {
        Ammo_UI.text = "Ammo: " + Bullets.ToString();
    }
    //Adds to total ammo if ammo item picked up
    public void AddAmmo(int Ammo)
    {
        Bullets += Ammo;
        Debug.Log("Added Ammo");
    }
}
