using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Prevents losing focus of UI elements due to mouse clicking
public class MouseInputBlocker : MonoBehaviour
{
    GameObject lastselectedObject;
    void Start()
    {
        lastselectedObject = new GameObject();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselectedObject);
        }
        else
        {
            lastselectedObject = EventSystem.current.currentSelectedGameObject;
        }

    }
}
