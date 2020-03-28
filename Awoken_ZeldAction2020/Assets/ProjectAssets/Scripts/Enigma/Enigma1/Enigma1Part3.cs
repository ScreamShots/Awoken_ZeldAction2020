using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Bastien Prigent
/// THis is the script of the first Enigma of our game
/// </summary>
public class Enigma1Part3 : MonoBehaviour
{
    [SerializeField]
    private DoorBehavior door;
    private ActionLever actionLever;



    void Start()
    {
        door = GetComponentInChildren<DoorBehavior>();
        actionLever = GetComponentInChildren<ActionLever>();
    }
    void Update()
    {

        OpenDoorActionLever();
    }


    void OpenDoorActionLever()
    {
        if (actionLever.isPressed == true)
        {
            door.isDoorOpen = true;
        }
        else if (actionLever.isPressed == false)
        {
            door.isDoorOpen = false;
        }
    }


}

