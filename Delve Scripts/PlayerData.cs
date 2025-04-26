using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public GameObject weaponPrefab;

    public Weapon(string name, GameObject prefab)
    {
        weaponName = name;
        weaponPrefab = prefab;
    }
}


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float maxHealth = 100f; // Default max health (can be adjusted)
    public bool hasHealthUpgraded = false; // Track if the upgrade has been applied

    public float currentHealth = 100f;
    public float maxXP = 100f;
    public float xp = 0f;
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float defaultWalkSpeed = 5f;
    public float sprintSpeed = 7f;
    public float upgradedWalkSpeed = 6f;
    public int healthLevel = 0;
    public int xpLevel = 0;
    public int damageReductionLevel = 0;
    public int staminaLevel = 0;
    public int speedLevel = 0;
    public int chargeSpeedLevel = 0;
    public int weaponDamageLevel = 0;
    public int fireRateLevel = 0;
    public int projectileSpeedLevel = 0;
    public int shieldDurabilityLevel = 0;
    public int shieldRechargeLevel = 0;

    // Array to store the weapons the player has picked up
    public List<Weapon> playerWeapons = new List<Weapon>(); // List to hold player's weapons

    // Method to add a weapon to the player's inventory
    public void AddWeapon(GameObject weaponPrefab)
    {
        // Avoid adding duplicates
        if (!playerWeapons.Exists(w => w.weaponPrefab == weaponPrefab))
        {
            string weaponName = weaponPrefab.name; // You can customize this to get a better name if needed
            playerWeapons.Add(new Weapon(weaponName, weaponPrefab));
        }
    }
    // Method to reset the player stats to default values
    public void ResetStats()
    {
        maxHealth = 100f;
        currentHealth = 100f;
        maxXP = 15f;
        xp = 0f;

        maxStamina = 100f;
        currentStamina = 100f;

        defaultWalkSpeed = 5f;
        sprintSpeed = 7f;
        upgradedWalkSpeed = 6f;

        healthLevel = 0;
        xpLevel = 0;
        damageReductionLevel = 0;
        staminaLevel = 0;
        speedLevel = 0;
        chargeSpeedLevel = 0;
        weaponDamageLevel = 0;
        fireRateLevel = 0;
        projectileSpeedLevel = 0;
        shieldDurabilityLevel = 0;
        shieldRechargeLevel = 0;

        // Clear the list of picked-up weapons
        playerWeapons.Clear();
    }

    // Inventory system
    [System.Serializable]
    public class Resource
    {
        public string resourceName;
        public int amount;
    }

    public List<Resource> resources = new List<Resource>();

    public void AddResource(string resourceName, int amount)
    {
        var resource = resources.Find(r => r.resourceName == resourceName);
        if (resource != null)
        {
            resource.amount += amount;
        }
        else
        {
            resources.Add(new Resource { resourceName = resourceName, amount = amount });
        }
    }

    public void RemoveResource(string resourceName, int amount)
    {
        var resource = resources.Find(r => r.resourceName == resourceName);
        if (resource != null)
        {
            resource.amount -= amount;
            if (resource.amount <= 0)
            {
                resources.Remove(resource);
            }
        }
    }

    public void DisplayInventory()
    {
        foreach (var resource in resources)
        {
            Debug.Log($"{resource.resourceName}: {resource.amount}");
        }
    }

    public void InstantiateWeapons(Transform weaponHolder)
    {
        foreach (var weapon in playerWeapons)
        {
            if (weapon.weaponPrefab != null)
            {
                weapon.weaponPrefab.SetActive(false);
                GameObject weaponInstance = Instantiate(weapon.weaponPrefab, weaponHolder);
                weaponInstance.transform.localPosition = new Vector3(0.5f, 0.2f, 0.5f);
                weaponInstance.transform.localRotation = Quaternion.identity;

                // Additional setup for the weapon instance can be done here
                Debug.Log($"Instantiated weapon: {weapon.weaponName}");
            }
            else
            {
                Debug.LogError("Weapon prefab is null!");
            }
        }
    }
}
