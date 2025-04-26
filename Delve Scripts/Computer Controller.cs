using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour {
    //These variables call for the UI display objects
    [SerializeField] private GameObject generalMenu;
    [SerializeField] private GameObject altarMenu;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject combatMenu;
    [SerializeField] private GameObject miningMenu;
    [SerializeField] private GameObject computerUI;

    //This variable is the collider for the player
    [SerializeField] private Collider playerCollider;

    //These variables control the on/off functionality for each menu
    private bool generalOn = false;
    private bool altarOn = false;
    private bool exitOn = false;
    private bool combatOn = false;
    private bool miningOn = false;

    //This boolean handles the trigger method functionality
    private bool isTriggered = false;

    //Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && isTriggered == true) {
            Debug.Log("Button Pressed");
            //General menu toggling
            if (name == "General") {
                if (!generalOn) {
                    generalMenu.SetActive(true);
                    computerUI.SetActive(false);
                    generalOn = true;
                }
                else if (generalOn) {
                    generalMenu.SetActive(false);
                    computerUI.SetActive(true);
                    generalOn = false;
                }
            }
            //Altar menu toggling
            if (name == "Altar") {
                if (!altarOn) {
                    altarMenu.SetActive(true);
                    computerUI.SetActive(false);
                    altarOn = true;
                }
                else if (altarOn) {
                    altarMenu.SetActive(false);
                    computerUI.SetActive(true);
                    altarOn = false;
                }
            }
            //Exit menu toggling
            if (name == "Exit") {
                if (!exitOn) {
                    exitMenu.SetActive(true);
                    computerUI.SetActive(false);
                    exitOn = true;
                }
                else if (exitOn) {
                    exitMenu.SetActive(false);
                    computerUI.SetActive(true);
                    exitOn = false;
                }
            }
            //Combat menu toggling
            if (name == "Combat") {
                if (!combatOn) {
                    combatMenu.SetActive(true);
                    computerUI.SetActive(false);
                    combatOn = true;
                }
                else if (combatOn) {
                    combatMenu.SetActive(false);
                    computerUI.SetActive(true);
                    combatOn = false;
                }
            }
            //Mining menu toggling
            if (name == "Mining") {
                if (!miningOn) {
                    miningMenu.SetActive(true);
                    computerUI.SetActive(false);
                    miningOn = true;
                }
                else if (miningOn) {
                    miningMenu.SetActive(false);
                    computerUI.SetActive(true);
                    miningOn = false;
                }
            }
        }
    }

    //This method checks for triggers in the detection area
    private void OnTriggerEnter(Collider collision) {
        //Debug.Log(collision.ToString());
        if (collision.gameObject.name == "PlayerObj") {
            isTriggered = true;
        }
    }

    //This method checks for when the player leaves the trigger area
    private void OnTriggerExit(Collider collision) {
        //Debug.Log(collision.ToString());
        if (collision.gameObject.name == "PlayerObj") {
            isTriggered = false;
        }
    }
}
