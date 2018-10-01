using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    void Awake()
    {
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    }

    void HandleException(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
