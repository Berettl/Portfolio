using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeFunctionality : MonoBehaviour
{
    public PlayerData playerData; // Reference to PlayerData ScriptableObject
    public PlayerMovement playerScript;
    public Inventory inventory;
    public  MiningLaserController laserController;
    public Gun gunRef;
    public ShieldBar shieldBar;
    public GameObject shieldBarUI;

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

    public TMP_Text healthUpgradePriceText;
    public TMP_Text xpUpgradePriceText;
    public TMP_Text DamageReducePriceText;
    public TMP_Text StaminaIncreasePriceText;
    public TMP_Text MovementIncreasePriceText;
    public TMP_Text ChargeSpeedIncreasePriceText;
    public TMP_Text WeaponDamageIncreasePriceText;
    public TMP_Text FireRateIncreasePriceText;
    public TMP_Text ProjectileIncreasePriceText;
    public TMP_Text ShieldDurabilityIncreasePriceText;
    public TMP_Text ShieldRechargePriceText;


    void Start()
    {
        playerScript = GetComponent<PlayerMovement>();
        laserController = GetComponent<MiningLaserController>();
        gunRef = laserController?.gun?.GetComponent<Gun>();
        shieldBar = shieldBarUI?.GetComponent<ShieldBar>();



        // Load player stats
        playerScript.maxHealth = playerData.maxHealth;
        playerScript.currentHealth = playerData.currentHealth;
        playerScript.maxXP = playerData.maxXP;
        playerScript.xp = playerData.xp;
        playerScript.maxStamina = playerData.maxStamina;
        playerScript.currentStamina = playerData.currentStamina;
        playerScript.defaultWalkSpeed = playerData.defaultWalkSpeed;
        playerScript.sprintSpeed = playerData.sprintSpeed;
        playerScript.upgradedWalkSpeed = playerData.upgradedWalkSpeed;

        healthLevel = playerData.healthLevel;
        xpLevel = playerData.xpLevel;
        damageReductionLevel = playerData.damageReductionLevel;
        staminaLevel = playerData.staminaLevel;
        speedLevel = playerData.speedLevel;
        chargeSpeedLevel = playerData.chargeSpeedLevel;
        weaponDamageLevel = playerData.weaponDamageLevel;
        fireRateLevel = playerData.fireRateLevel;
        projectileSpeedLevel = playerData.projectileSpeedLevel;
        shieldDurabilityLevel = playerData.shieldDurabilityLevel;
        shieldRechargeLevel = playerData.shieldRechargeLevel;

        // Debug inventory
        playerData.DisplayInventory();
    }

    void SaveStats()
    {
        playerData.maxHealth = playerScript.maxHealth;
        playerData.currentHealth = playerScript.currentHealth;
        playerData.maxXP = playerScript.maxXP;
        playerData.xp = playerScript.xp;
        playerData.maxStamina = playerScript.maxStamina;
        playerData.currentStamina = playerScript.currentStamina;
        playerData.defaultWalkSpeed = playerScript.defaultWalkSpeed;
        playerData.sprintSpeed = playerScript.sprintSpeed;
        playerData.upgradedWalkSpeed = playerScript.upgradedWalkSpeed;

        playerData.healthLevel = healthLevel;
        playerData.xpLevel = xpLevel;
        playerData.damageReductionLevel = damageReductionLevel;
        playerData.staminaLevel = staminaLevel;
        playerData.speedLevel = speedLevel;
        playerData.chargeSpeedLevel = chargeSpeedLevel;
        playerData.weaponDamageLevel = weaponDamageLevel;
        playerData.fireRateLevel = fireRateLevel;
        playerData.projectileSpeedLevel = projectileSpeedLevel;
        playerData.shieldDurabilityLevel = shieldDurabilityLevel;
        playerData.shieldRechargeLevel = shieldRechargeLevel;
    }

    public void UpdateUpgradePriceText(TMP_Text upgradePriceText, int level, string resourceName)
    {
        upgradePriceText.text = "Cost:\n" + (2+level).ToString() + " " + resourceName + "\n" + (2+level).ToString() + " " + "XP";
    }

    public void XPChange()
    {
        playerScript.XPBar.fillAmount = playerScript.xp / playerScript.maxXP;
        if (playerScript.xp > playerScript.maxXP)
        {
            playerScript.xp = playerScript.maxXP;
        }
        playerScript.xpAmount.text = playerScript.xp.ToString();
    }

    // Sub-Combat Section

    public void HealthIncrease()
    {
        if (playerScript.xp >= (1 + healthLevel) && playerData.resources.Exists(r => r.resourceName == "Ruby" && r.amount >= (1 + healthLevel)) && healthLevel != 20)
        {
            playerScript.xp -= 1 + healthLevel;

            XPChange();

            playerData.RemoveResource("Ruby", 1 + healthLevel);

            UpdateUpgradePriceText(healthUpgradePriceText, healthLevel, "Ruby");

            playerScript.maxHealth += 5;
            healthLevel++;
            playerScript.currentHealth = playerScript.maxHealth;
            playerScript.healthBar.fillAmount = playerScript.currentHealth / playerScript.maxHealth;

            playerScript.healthBar.rectTransform.sizeDelta = new Vector2(350f + (healthLevel * 12), 48.5f);
            playerScript.healthBarBG.rectTransform.sizeDelta = new Vector2(350f + (healthLevel * 12), 48.5f);

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    public void XPIncrease()
    {
        if (playerScript.xp >= (1 + xpLevel) && playerData.resources.Exists(r => r.resourceName == "Ruby" && r.amount >= (1 + xpLevel)) && xpLevel != 20)
        {
            playerScript.xp -= 1 + xpLevel;

            XPChange();

            playerData.RemoveResource("Ruby", 1 + xpLevel);
            UpdateUpgradePriceText(xpUpgradePriceText, xpLevel, "Ruby");

            playerScript.maxXP += 5;
            xpLevel++;
            playerScript.XPBar.fillAmount = playerScript.xp / playerScript.maxXP;

            playerScript.healthBar.rectTransform.sizeDelta = new Vector2(350f + (healthLevel * 12), 48.5f);
            playerScript.healthBarBG.rectTransform.sizeDelta = new Vector2(350f + (healthLevel * 12), 48.5f);

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    public void DamageReductionIncrease()
    {
        if (playerScript.xp >= (1 + damageReductionLevel) && playerData.resources.Exists(r => r.resourceName == "Ruby" && r.amount >= (1 + damageReductionLevel)) && damageReductionLevel != 20)
        {
            playerScript.xp -= 1 + damageReductionLevel;

            XPChange();

            playerData.RemoveResource("Ruby", 1 + damageReductionLevel);
            UpdateUpgradePriceText(DamageReducePriceText, damageReductionLevel, "Ruby");

            playerScript.damageReduction -= 0.01f;
            damageReductionLevel++;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    // Movement Section
    public void StaminaIncrease()
    {
        if (playerScript.xp >= (1 + staminaLevel) && playerData.resources.Exists(r => r.resourceName == "Emerald" && r.amount >= (1 + staminaLevel)) && staminaLevel != 20)
        {
            playerScript.xp -= 1 + staminaLevel;

            XPChange();

            playerData.RemoveResource("Emerald", 1 + staminaLevel);
            UpdateUpgradePriceText(StaminaIncreasePriceText, staminaLevel, "Emerald");

            playerScript.maxStamina += 5;
            staminaLevel++;
            playerScript.currentStamina = playerScript.maxStamina;
            playerScript.staminaBar.fillAmount = playerScript.currentStamina / playerScript.maxStamina;

            playerScript.staminaBar.rectTransform.sizeDelta = new Vector2(350f + (staminaLevel * 12), 48.5f);
            playerScript.staminaBarBG.rectTransform.sizeDelta = new Vector2(350f + (staminaLevel * 12), 48.5f);

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    public void MovementIncrease()
    {
        if (playerScript.xp >= (1 + speedLevel) && playerData.resources.Exists(r => r.resourceName == "Emerald" && r.amount >= (1 + speedLevel)) && speedLevel != 20)
        {
            playerScript.xp -= 1 + speedLevel;

            XPChange();

            playerData.RemoveResource("Emerald", 1 + speedLevel);
            UpdateUpgradePriceText(MovementIncreasePriceText, speedLevel, "Emerald");

            playerScript.defaultWalkSpeed += 1f;
            playerScript.upgradedWalkSpeed += 1f;
            playerScript.sprintSpeed += 1f;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    public void ChargeSpeedIncrease()
    {
        if (playerScript.xp >= (1 + chargeSpeedLevel) && playerData.resources.Exists(r => r.resourceName == "Emerald" && r.amount >= (1 + chargeSpeedLevel)) && chargeSpeedLevel != 20)
        {
            playerScript.xp -= 1 + chargeSpeedLevel;

            XPChange();

            playerData.RemoveResource("Emerald", 1 + chargeSpeedLevel);
            UpdateUpgradePriceText(ChargeSpeedIncreasePriceText, chargeSpeedLevel, "Emerald");

            playerScript.chargeRate += 5f;
            chargeSpeedLevel++;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    // Weapon Upgrade Section

    public void WeaponDamageIncrease(){
        gunRef = laserController.gun.GetComponent<Gun>();
        if (playerScript.xp >= (1 + weaponDamageLevel) && playerData.resources.Exists(r => r.resourceName == "Iron Ore" && r.amount >= (1 + weaponDamageLevel)) && weaponDamageLevel != 20)
        {
            playerScript.xp -= 1 + weaponDamageLevel;

            XPChange();

            playerData.RemoveResource("Iron Ore", 1 + weaponDamageLevel);
            UpdateUpgradePriceText(WeaponDamageIncreasePriceText, weaponDamageLevel, "Iron Ore");

            gunRef.Damage += 1;
            weaponDamageLevel++;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }

    public void FireRateIncrease(){
        gunRef = laserController.gun.GetComponent<Gun>();
        if (playerScript.xp >= (1 + fireRateLevel) && playerData.resources.Exists(r => r.resourceName == "Iron Ore" && r.amount >= (1 + fireRateLevel)) && fireRateLevel != 20)
        {
            playerScript.xp -= 1 + fireRateLevel;

            XPChange();

            playerData.RemoveResource("Iron Ore", 1 + fireRateLevel);
            UpdateUpgradePriceText(FireRateIncreasePriceText, fireRateLevel, "Iron Ore");

            gunRef.fireRate += 1;
            fireRateLevel++;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }
    /*
    public void ProjectileSpeedIncrease(){
        gunRef = laserController.gun.GetComponent<Gun>();
        if (playerScript.xp >= (1 + projectileSpeedLevel) && playerData.resources.Exists(r => r.resourceName == "Iron Ore" && r.amount >= (1 + projectileSpeedLevel)) && projectileSpeedLevel != 20)
        {
            playerScript.xp -= 1 + projectileSpeedLevel;

            XPChange();

            playerData.RemoveResource("Iron Ore", 1 + projectileSpeedLevel);
            UpdateUpgradePriceText(ProjectileIncreasePriceText, projectileSpeedLevel, "Iron Ore");

            //gunRef.bulletVelocity += 5;
            projectileSpeedLevel++;

            SaveStats(); // Save changes to PlayerData
        }
        else
        {
            Debug.LogWarning("Insufficient resources. Please collect more.");
        }
    }
    */

    // Sheild Section

    //Rebekah added the shield public method for increasing shield durability
    public void ShieldDurabilityIncrease() {
        if (!shieldBar.GetMaxDurability()) {
            if (playerScript.xp >= (1 + shieldDurabilityLevel) && playerData.resources.Exists(r => r.resourceName == "Sapphire" && r.amount >= (1 + shieldDurabilityLevel)) && shieldDurabilityLevel != 20)
            {
                playerScript.xp -= 1 + shieldDurabilityLevel;

                XPChange();

                playerData.RemoveResource("Sapphire", 1 + shieldDurabilityLevel);
                UpdateUpgradePriceText(ShieldDurabilityIncreasePriceText, shieldDurabilityLevel, "Sapphire");

                shieldBar.IncreaseDurability();
                shieldDurabilityLevel++;

                SaveStats(); // Save changes to PlayerData
            }
            else {
                Debug.LogWarning("Insufficient resources. Please collect more.");
            }
        }
        else {
            Debug.Log("Max shield durability already reached");
        }
    }

    // //Rebekah added the shield public method for increasing shield recharge rate
    public void ShieldRechargeIncrease() {
        if (!shieldBar.GetMaxRecharge()) {
            if (playerScript.xp >= (1 + shieldRechargeLevel) && playerData.resources.Exists(r => r.resourceName == "Sapphire" && r.amount >= (1 + shieldRechargeLevel)) && shieldRechargeLevel != 20)
            {
                playerScript.xp -= 1 + shieldRechargeLevel;

                XPChange();

                playerData.RemoveResource("Sapphire", 1 + shieldRechargeLevel);
                UpdateUpgradePriceText(ShieldRechargePriceText, shieldRechargeLevel, "Sapphire");

                shieldBar.IncreaseRechargeRate();
                shieldRechargeLevel++;

                SaveStats(); // Save changes to PlayerData
            }
            else {
                Debug.LogWarning("Insufficient resources. Please collect more.");
            }
        }
        else {
            Debug.Log("Max shield recharge rate already reached.");
        }
    }
}
