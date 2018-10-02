using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        QualitySettings.maxQueuedFrames = 1;
    }
}
