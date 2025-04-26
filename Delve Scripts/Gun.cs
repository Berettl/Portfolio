using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawn;           // Spawn point for bullets
    public float fireRate = 0.5f;           // Fire rate in bullets per second
    private float nextFireTime = 0f;        // Tracks when next bullet can be fired
    public int Damage = 1;                  // Damage for each shot
    public int maxAmmo = 5;
    public int currentAmmo;
    public float reloadTime = 2f;
    public bool isReloading = false;
    private bool isFiring = false;
    public float range = 50f;               // Maximum range for hitscan
    public float impactForce = 10f;         // Force applied to hit objects

    public KeyCode reloadKey = KeyCode.R;
    public LayerMask hitMask;               // Define what objects the bullets can hit
    public GameObject particleFX;
    // Shotgun settings
    public bool isShotgun = false;
    public int pelletCount = 8;
    public float spreadAngle = 10f;

    public TMP_Text ammoText; // UI Text to display Bullet Count


    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        ammoText = GameObject.Find("BulletText")?.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!PauseMenu.isGamePaused && !AltarInteraction.isGamePaused) {
            if (isReloading)
                return;

            if (Input.GetMouseButton(0) && Time.time > nextFireTime && currentAmmo > 0) {
                FireWeapon();
                particleFX.SetActive(true);
                nextFireTime = Time.time + 1f / fireRate;
            }

            if (!Input.GetMouseButton(0) || currentAmmo == 0) {
                particleFX.SetActive(false);
            }

            if (Input.GetKeyDown(reloadKey) && currentAmmo < maxAmmo) {
                StartCoroutine(Reload());
            }
        }
        else {
            StopAllCoroutines();
            particleFX.SetActive(false);
            isReloading = false;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reloaded");
        UpdateAmmoUI();
    }

    private void FireWeapon()
    {
        //Rebekah added the if statement to check if the public static bool isGamePaused is false,
        //so weapons can't be fired while the game is paused
        if (PauseMenu.isGamePaused == false) {
            if (gameObject.transform.parent.name == "Weaponholder") {
                GetComponent<AudioSource>().Play();

                if (isShotgun) {
                    FireShotgun();
                }
                else {
                    FireHitscanBullet();
                }

                currentAmmo--;
                UpdateAmmoUI();
            }
        }
        else {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void FireHitscanBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, range, hitMask))
        {
            Debug.Log("Hit: " + hit.collider.name);

            NewBehaviourScript enemy = hit.collider.GetComponent<NewBehaviourScript>();
            if (enemy != null)
            {
                enemy.RemoveHealth(Damage);
            }
        }
    }

    private void FireShotgun()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spreadDirection = bulletSpawn.forward +
                                      new Vector3(Random.Range(-spreadAngle, spreadAngle) / 100f,
                                                  Random.Range(-spreadAngle, spreadAngle) / 100f,
                                                  0);

            RaycastHit hit;
            if (Physics.Raycast(bulletSpawn.position, spreadDirection, out hit, range, hitMask))
            {
                Debug.Log("Shotgun hit: " + hit.collider.name);

                NewBehaviourScript enemy = hit.collider.GetComponent<NewBehaviourScript>();
                if (enemy != null)
                {
                    enemy.RemoveHealth(Damage);
                }
            }
        }
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
        }
    }
}