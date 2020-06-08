using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFocusLocker : MonoBehaviour
{
    GameObject lastselect;

    void Update()
    {
        if(EventSystem.current != null)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(lastselect);
            }
            else if (lastselect != EventSystem.current.currentSelectedGameObject)
            {
                lastselect = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
