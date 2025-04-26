using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLevelButtons : MonoBehaviour
{
    public GameObject Button;

    public GameObject MazeQuad;
    public GameObject MazeQuadVar;

    private Vector3 mazeQuadPosition;
    private Vector3 mazeQuadVarPosition;

    private bool isPlayerInRange = false; // Flag to track if player is in range
    private bool isSwapped = false; //Flag to track if quads are swapped
    [SerializeField] private AudioSource stoneMorph;

    void Start()
    {
        mazeQuadPosition = MazeQuad.transform.position;
        mazeQuadVarPosition = MazeQuadVar.transform.position;
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player Pressed E");
            QuadSwap();
            stoneMorph.Play();
        }
    }

    public void QuadSwap()
    {
        if (isSwapped)
        {
            // Swap back to the original positions
            MazeQuad.transform.position = mazeQuadPosition;
            MazeQuadVar.transform.position = mazeQuadVarPosition;
        }
        else
        {
            // Swap to the alternative positions
            MazeQuad.transform.position = mazeQuadVarPosition;
            MazeQuadVar.transform.position = mazeQuadPosition;
        }

        // Toggle the swapped state
        isSwapped = !isSwapped;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player In Range");
            isPlayerInRange = true; // Set flag when player enters range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Reset flag when player leaves range
        }
    }
}
