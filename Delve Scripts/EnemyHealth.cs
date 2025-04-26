using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/**
 * 
 * Version: 08/31/2024 
 * 
 * Author: Rebekah Bledsoe
 * 
 * Summary: This script controls the main functions for the enemy's
 * health and any related method to that. For testing purposes, the
 * user can press the 'T' key to check that the health removal works,
 * but the script is also set up to check for collisions with the 
 * player's weapon.
 * 
 **/

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 100;
    //Xiong Edit
    [SerializeField] private Slider healthbar;
    
    [SerializeField] private Text enemyHealthText;
    [SerializeField] private Canvas enemyHealthCanvas;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float takeDamageCooldown = 1.0f;
    public int xpDrop;
    public GameObject playerObject;
    public PlayerMovement playerScript;
    private EnemyTest1 enemyAI; // Reference to the enemy AI script

    private StateMachine stateMachine; //Connect to Xiong StateMachine

    //Rebekah added this bool to allow for random room spawn encounters
    private bool isRandomEnemy = false;

    //Rebekah added this local variable for each enemy to reference the spawn enemy scipt
    private EnemySpawnerTrigger enemySpawnTrigger;

    //Rebekah added this public method to set the isRandomEnemy bool for random room spawn encounter functionality
    public void SetRandomEnemyStatus(bool randomStatus) {
        isRandomEnemy = randomStatus;
    }

    //Rebekah added this public method to set the reference for the different SpawnEnemy script for each enemy
    public void SetSpawnEnemyScript(EnemySpawnerTrigger enemySpawnScript) {
        enemySpawnTrigger = enemySpawnScript;
    }

    // The start method is required to find both the PlayerObject and the Object's Movement script.

    void Start()
    {
        playerObject = GameObject.Find("PlayerObj");
        playerScript = playerObject.GetComponent<PlayerMovement>();
        enemyAI = GetComponent<EnemyTest1>(); // Get the enemy AI component
    }

    /**
     * This method is called once per frame, and it checks 
     * if the enemy still has health. If so, the user
     * can press the Key 'T' in order to test the health removal, 
     * which can be removed once the script is reviewed and 
     * works with the player's weapon pickaxe. If the enemy
     * has no more health, it will call the Die method.
     **/

    void Update()
    {
        //Xiong Edit
        healthbar.value = enemyHealth;
        
        //Debug.Log("Enemy Health = " + enemyHealth);
        enemyHealthCanvas.transform.position = gameObject.transform.position;
        enemyHealthText.text = "Enemy Health = " + enemyHealth;

        if (enemyHealth > 0) {
            if (Input.GetKeyDown(KeyCode.T)) {
                RemoveHealth(1);
            }
        }
        else {
            Die();
        }
    }


    /**
     * This method checks for collisions, and when one happens,
     * if the colliding object is tagged as a weapon, it calls
     * the remove health method.
     **/
    /**
    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag("Weapon")) {
            if (canTakeDamage)
            {
                Debug.Log("Hit");
                RemoveHealth(1);
                canTakeDamage = false;  // Disable further damage until cooldown
                StartCoroutine(ResetTakeHitCooldown());
            }
        }
    }
    **/


    //This public method can be accessed by any script in order to remove the enemy's health
    public void RemoveHealth(int removalAmount) {
        enemyHealth -= removalAmount;
        
        if (CompareTag("Enemy"))
        {
            if (stateMachine != null)
            {
                
                var searchState = new SearchState(stateMachine as EnemyStateMachine);
                stateMachine.SwitchState(searchState);
            }
            else
            {
                enemyAI.SetAggro();
                enemyAI.AggroNearbyEnemies();
            }
        }
    }


    //This public method can be accessed by any script in order to kill the enemy and add the amount of XP to the player's XP counter.
    public void Die() {
        playerScript.xp += xpDrop;
        Debug.Log($"XP: {playerScript.xp}");
        if (isRandomEnemy) {
            enemySpawnTrigger.ChangeEnemiesLeft(-1);
        }
        Destroy(gameObject);

        playerScript.XPBar.fillAmount = playerScript.xp / playerScript.maxXP;

        if(playerScript.xp > playerScript.maxXP){
            playerScript.xp = playerScript.maxXP;
        }

        playerScript.xpAmount.text = playerScript.xp.ToString();
    }

    IEnumerator ResetTakeHitCooldown()
    {
        yield return new WaitForSeconds(takeDamageCooldown);
        canTakeDamage = true;
    }
}
