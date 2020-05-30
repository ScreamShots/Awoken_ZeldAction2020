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
    [SerializeField]
    private EdgeCollider2D[] edgeDoors;


    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        edgeDoors = GetComponentsInChildren<EdgeCollider2D>();
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
            for(int i = 0; i < edgeDoors.Length; i++)
            {
                edgeDoors[i].enabled = false;
            }
        }
    }

    void DoorIsOpen()
    {
        if (isDoorOpen == true)
        {
            doorCollider.enabled = false;
            for (int i = 0; i < edgeDoors.Length; i++)
            {
                edgeDoors[i].enabled = true;
            }
        }
    }
}
