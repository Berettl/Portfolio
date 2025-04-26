using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiningLaserController : MonoBehaviour /*,IPointerDownHandler, IPointerUpHandler*/
{
    public GameObject miningLaser;
    public GameObject beam;
    public GameObject miniGameUI;
    public GameObject pickaxe;
    //Rebekah added this code for addition gun functionality
    public GameObject gun;

    //Rebekah added this code for audio functionality
    [SerializeField] private AudioSource beamAudio;

    public PickaxeController pickaxeController;

    bool laserActivated = false;

    //Rebekah added this bool for proper weapon switching
    private bool isGun = false;

    // Start is called before the first frame update
    void Start()
    {
        //miningLaser.gameObject.SetActive(false);
        beamAudio.Stop();
        beam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Rebekah added the if statement to check if the public static bool isGamePaused is false,
        //so weapons can't be fired while the game is paused
        if (PauseMenu.isGamePaused == false && !AltarInteraction.isGamePaused) {
            //Rebekah added these lines for proper gun switching
            //The bool will be determined by what weapon is active in the scene
            //This will allow for switching back to the correct weapon
            if (gun.activeInHierarchy) {
                isGun = true;
            }
            else if (pickaxe.activeInHierarchy) {
                isGun = false;
            }

            /**
            if(Input.GetKeyDown(KeyCode.L) && !pickaxeController.isAttacking){
                laserActivated = !laserActivated;
                if(laserActivated){
                    miningLaser.gameObject.SetActive(true);
                    pickaxe.gameObject.SetActive(false);

                    //Rebekah added this to set the gun object false when switching to the laser
                    gun.gameObject.SetActive(false);
                }
                else
                {
                    miningLaser.gameObject.SetActive(false);

                    //Rebekah replaced the pickaxe activation with this if statement
                    //This is so the player switches back to the correct weapon
                    if (isGun) {
                        gun.gameObject.SetActive(true);
                    }
                    else {
                        pickaxe.gameObject.SetActive(true);
                    }
                }
            }
            **/

            if (Input.GetKey(KeyCode.Mouse0)) {
                //Rebekah added the if statement to check if the game is paused
                if ((AltarInteraction.isGamePaused == false || PauseMenu.isGamePaused == false)) {
                    if (Input.GetKeyDown(KeyCode.Mouse0)) {
                        beamAudio.Play();
                        beam.gameObject.SetActive(true);
                    }
                }
            }
            else {
                beamAudio.Stop();
                miniGameUI.gameObject.SetActive(false);
                beam.gameObject.SetActive(false);

            }
        }
        else {
            beamAudio.Stop();
            beam.gameObject.SetActive(false);
        }
    }
}
