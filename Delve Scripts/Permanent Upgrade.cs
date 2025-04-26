using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PermanentUpgrade : MonoBehaviour
{
    public PlayerData playerData; // Reference to PlayerData ScriptableObject
    public PlayerMovement playerMovement; // Reference to PlayerMovement for health updates
    public Inventory inventory; // Reference to the player's inventory
    public TMP_Text healthUpgradePriceText; // UI Text to display the upgrade cost

    private int healthUpgradeCost = 1; // Cost in XP for health upgrade
    private string upgradeResourceName = "Ruby"; // Resource required for the upgrade

    private void Start()
    {
        // Load health from PlayerData when the game starts
        LoadPlayerHealth();
        UpdateUpgradePriceText();
    }

    public void HealthIncrease()
    {
        // Check if the upgrade has already been applied using the PlayerData
        if (playerData.hasHealthUpgraded)
        {
            Debug.LogWarning("Health upgrade has already been applied!");
            return; // Exit if the upgrade has been used
        }

        if (playerMovement.xp >= healthUpgradeCost && inventory.resources.ContainsKey(upgradeResourceName) && inventory.resources[upgradeResourceName] >= 1)
        {
            // Deduct resources for the upgrade
            playerMovement.xp -= healthUpgradeCost;
            inventory.RemoveResource(upgradeResourceName, 1);

            // Call the DoubleHealth method
            DoubleHealth();

            // Mark the upgrade as used in PlayerData
            playerData.hasHealthUpgraded = true;

            // Update UI or other elements as needed
            UpdateUpgradePriceText();

            Debug.Log("Health upgrade successful!");
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    private void DoubleHealth()
    {
        // Double the player's maximum health
        playerData.maxHealth *= 2;
        playerMovement.currentHealth = playerData.maxHealth; // Update current health to match the new max

        Debug.Log("Player health has been doubled!");
    }

    private void LoadPlayerHealth()
    {
        // Set current health based on max health from PlayerData
        playerMovement.currentHealth = playerData.maxHealth;
    }

    private void UpdateUpgradePriceText()
    {
        // Update the UI text to show the cost of the upgrade
        healthUpgradePriceText.text = playerData.hasHealthUpgraded
            ? "Health upgrade already applied!"
            : $"Cost:\n{healthUpgradeCost} {upgradeResourceName}\n{healthUpgradeCost} XP";
    }
}
