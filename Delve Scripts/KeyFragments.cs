using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFragments : MonoBehaviour
{
    private bool isActivated = false; // Tracks if the pillar has been used

    public Material activatedMaterial; // Material with emission enabled
    private Renderer pillarRenderer;

    private void Start()
    {
        // Get the Renderer component to change the material later
        pillarRenderer = GetComponent<Renderer>();
    }

    public void Interact()
    {
        if (isActivated)
        {
            Debug.Log("This pillar has already been used!");
            return; // Prevent further interaction
        }

        isActivated = true; // Mark the pillar as used
        Debug.Log("Key fragment collected!");

        // Add your logic for giving the player a key fragment here
        PlayerKeyManager.Instance.AddKeyFragment();

        // Change the material to signify activation
        if (activatedMaterial != null)
        {
            pillarRenderer.material = activatedMaterial;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
}
