using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyManager : MonoBehaviour
{
    public static PlayerKeyManager Instance;

    public int keyFragmentCount = 0; // Number of collected key fragments
    public int requiredKeys = 3; // Number of keys needed to unlock the scene loader

    private void Awake()
    {
        Instance = this;
    }

    public void AddKeyFragment()
    {
        keyFragmentCount++;
        Debug.Log($"Key fragments: {keyFragmentCount}/{requiredKeys}");

        if (keyFragmentCount >= requiredKeys)
        {
            UnlockSceneLoader();
        }
    }

    private void UnlockSceneLoader()
    {
        Debug.Log("Scene loader unlocked!");
        // Add your scene loading logic here
    }
}
