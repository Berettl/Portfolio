using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameObject player;
    public Transform returnPoint;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Player entered Dead Zone");
            player.transform.position = returnPoint.transform.position;
            playerMovement.Hurt(25);
        }
    }
}
