using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBackgroundAnimation : MonoBehaviour
{
    [SerializeField] private int rotationDelta = -25;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationDelta*Time.deltaTime);
    }
}
