using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float currentHealth;
    public float maxHealth;
    public float defaultWalkSpeed = 4f;
    public float upgradedWalkSpeed;
    public float sprintSpeed = 8f;
    public bool isSprinting = false;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float xp;
    public float maxXP;
    public float lookSpeed = 2f;
    public float lookXLimit = 85f;
    public Image XPBar;
    public TMP_Text xpAmount;
    public Image staminaBar;
    public Image healthBar;
    public Image healthBarBG;
    public Image staminaBarBG;
    public float currentStamina;
    public float maxStamina;
    public float sprintCost;
    public float chargeRate;
    public float damageReduction = 1;
    private Coroutine recharge;
    public ShieldBar shieldBar;

    //Rebekah added these variables for audio functionality
    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource sprintAudio;
    [SerializeField] private AudioSource hurtAudio;
    [SerializeField] private AudioSource outOfBreathAudio;

    private bool isJumped = false;

    public GameObject deathScreen;
    public bool isAlive;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    CharacterController characterController;
    void Start()
    {
        // Calls for the character controller in order to actually allow everything to function.
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        upgradedWalkSpeed = defaultWalkSpeed;
        xp = 0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isAlive = true;
        currentHealth = maxHealth;
    }

    void Update() {
        //Rebekah added the if statement to check if the public static bool isGamePaused is false,
        //so the player can't move/audio will stop playing once the game is paused
        if (!AltarInteraction.isGamePaused && PauseMenu.isGamePaused == false) {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            //Check for sprint input
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                isSprinting = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0) {
                isSprinting = false;
            }

            //Set/Reset movement speed depending on if the player is sprinting.
            if (isSprinting) {
                defaultWalkSpeed = sprintSpeed;

                currentStamina -= sprintCost * Time.deltaTime;
                if (currentStamina < 0) currentStamina = 0;
                staminaBar.fillAmount = currentStamina / maxStamina;

                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());

                if (currentStamina <= 0) {
                    outOfBreathAudio.Play();
                }
            }
            else if (!isSprinting) {
                defaultWalkSpeed = upgradedWalkSpeed;
            }

            //Character controller for audio playing
            if (characterController.velocity != Vector3.zero) {
                if (!isSprinting) {
                    if (!walkAudio.isPlaying) {
                        if (!isJumped) {
                            walkAudio.Play();
                            sprintAudio.Stop();
                        }
                    }
                }
                else {
                    if (!sprintAudio.isPlaying) {
                        if (!isJumped) {
                            sprintAudio.Play();
                            walkAudio.Stop();
                        }
                    }
                }
            }
            else {
                walkAudio.Stop();
                sprintAudio.Stop();
            }

            // Obtains the inputs of the player and moves accordingly. Also clarifies what y axis the player is on for future use.
            float curSpeedX = defaultWalkSpeed * Input.GetAxis("Vertical");
            float curSpeedY = defaultWalkSpeed * Input.GetAxis("Horizontal");
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Obtains the jump input, but only registers if the character controller is on the ground. Sets the y axis to the jump power, thus making you jump.
            if (Input.GetButton("Jump") && characterController.isGrounded) {
                moveDirection.y = jumpPower;
            }
            else {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded) {
                moveDirection.y -= gravity * Time.deltaTime;
                walkAudio.Stop();
                sprintAudio.Stop();
                isJumped = true;
            }
            else {
                isJumped = false;
            }


            // Obtains mouse inputs for camera movement, limits the rotation speed, clamps the vertical camera to two limits, and allows for the actual movement of the camera.
            if ((AltarInteraction.isGamePaused || PauseMenu.isGamePaused) == false && isAlive) {
                characterController.Move(moveDirection * Time.deltaTime);
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            if (currentHealth <= 0) {
                Death();
            }
        }
        else {
            walkAudio.Stop();
            sprintAudio.Stop();
            hurtAudio.Stop();
            outOfBreathAudio.Stop();
            isSprinting = false;
        }
    }

    //Recharge stamina @ chargeRate/s 2 seconds after sprinting ends.
    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(2f);

        while(currentStamina < maxStamina){
            currentStamina += chargeRate / 10f;
            if(currentStamina > maxStamina) currentStamina = maxStamina;
            staminaBar.fillAmount = currentStamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
    
    public void Death()
    {
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isAlive = false;
    }
    
    public void Hurt(int damage)
    {
        if (shieldBar.GetShield() == false) {
            hurtAudio.Play();
            currentHealth -= damage;
            healthBar.fillAmount = currentHealth / maxHealth;

            Debug.Log($"Health: {currentHealth}");
        }
        if (currentHealth <= 0)
        {
            Death();
        }
        else {
            shieldBar.SetChargeFill();
            shieldBar.StartShieldRecharge();
        }
    }
}
