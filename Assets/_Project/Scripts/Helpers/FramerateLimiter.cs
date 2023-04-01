using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script is only for testing purposes, simulating the game performance on different framerates.
 * This should not be implemented in a final build, but if it is left by error, the script won't run on a final build.
 */
public class FramerateLimiter : MonoBehaviour
{
    [SerializeField]
    private int desiredFrameRate = 60;

    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // Do not wait for any kind of sync.
        Application.targetFrameRate = desiredFrameRate; // Limit application to desired framerate
#endif
    }
}
