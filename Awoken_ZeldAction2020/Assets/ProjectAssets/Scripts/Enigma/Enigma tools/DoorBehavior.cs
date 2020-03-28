using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Bastien Prigent
/// This script Open and Closes differents enigma door
/// </summary>
public class DoorBehavior : MonoBehaviour
{
    public bool isDoorOpen;
    [SerializeField]
    private BoxCollider2D doorCollider;


    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        DoorIsClosed();
        DoorIsOpen();
    }
    void DoorIsClosed()
    {
        if (isDoorOpen == false)
        {
            doorCollider.enabled = true;
        }
    }

    void DoorIsOpen()
    {
        if (isDoorOpen == true)
        {
            doorCollider.enabled = false;
        }
    }
}
