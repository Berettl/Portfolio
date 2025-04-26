using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public PlayerData playerData; // Reference to PlayerData ScriptableObject
    public GameObject miningLaser;
    public GameObject selectedGunPrefab;  // The weapon prefab (visual and functional model) to display and pick up
    public Transform weaponHolder;  // The weapon holder object (optional)

    private GameObject gunInstance;  // The instance of the weapon that will appear visually in the scene

    public GameObject[] displayPrefabs;

    void Start()
    {
        playerData.InstantiateWeapons(weaponHolder);

        selectedGunPrefab = displayPrefabs[Random.Range(0, displayPrefabs.Length)];

        if (selectedGunPrefab != null)
        {
            gunInstance = Instantiate(selectedGunPrefab, transform);
            gunInstance.transform.localPosition = Vector3.zero;
            gunInstance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("GunPrefab is null!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (weaponHolder != null)
            {
                MiningLaserController activeGunDetection = other.GetComponent<MiningLaserController>();
                GameObject newGun = Instantiate(selectedGunPrefab, weaponHolder);

                // Clear the current gun and set the new one
                Destroy(activeGunDetection.gun);
                activeGunDetection.gun = newGun;
                miningLaser.SetActive(false);

                newGun.transform.localPosition = new Vector3(0.5f, 0.2f, 0.5f);
                newGun.transform.localRotation = Quaternion.identity;

                // Remove physics components from the new gun
                Rigidbody gunRb = newGun.GetComponent<Rigidbody>();
                if (gunRb != null) Destroy(gunRb);

                Collider gunCollider = newGun.GetComponent<Collider>();
                if (gunCollider != null) Destroy(gunCollider);

                // Add the new weapon to the player's weapon list in PlayerData
                if (playerData != null)
                {
                    playerData.AddWeapon(selectedGunPrefab); // Use the new method
                }

                // Optionally, disable or destroy this pickup object
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Weapon holder not found!");
            }
        }
    }
}
