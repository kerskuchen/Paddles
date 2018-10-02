using UnityEngine;

// The ExceptionHandler shuts down the game if an exception has been thrown somewhere.
// This way we instantly know that something went wrong somewhere.
public class ExceptionHandler : MonoBehaviour
{
    void Awake()
    {
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    }

    void HandleException(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error || type == LogType.Assert)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // TODO(JaSc): Maybe show some dialog box with stacktrace here?
			Application.Quit();
#endif
        }
    }
}
