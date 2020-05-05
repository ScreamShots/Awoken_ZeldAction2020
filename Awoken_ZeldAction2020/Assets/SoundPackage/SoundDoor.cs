using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Steve Guitton
/// Ce script permets de gérer les conditions d'activations des différents sons des portes
/// </summary>

public class SoundDoor : MonoBehaviour
{
    private DoorBehavior scriptDoor;

    private bool l_isDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        scriptDoor = GetComponentInParent<DoorBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(l_isDoorOpen != scriptDoor.isDoorOpen)
        {
            if(scriptDoor.isDoorOpen == true)
            {
                DoorOpen();
            }
            l_isDoorOpen = scriptDoor.isDoorOpen;
        }
    }

    void DoorOpen()
    {
        SoundManager.Instance.Play("OpenDoor");
    }
}
