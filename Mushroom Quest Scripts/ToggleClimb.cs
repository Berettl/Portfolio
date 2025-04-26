using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleClimb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.transform.GetComponent<CharacterController2D>().ladderClimbingAbility.canClimbLadder)
        {
            collision.transform.GetComponent<CharacterController2D>().climbSpeed = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.GetComponent<CharacterController2D>().ladderClimbingAbility.canClimbLadder)
        {
            collision.transform.GetComponent<CharacterController2D>().climbSpeed = 0;
        }
    }
}
