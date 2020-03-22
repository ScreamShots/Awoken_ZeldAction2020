using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Bastien Prigent
/// THis is the script of the first Enigma of our game
/// </summary>
public class Enigma1Part1 : MonoBehaviour
{
    [SerializeField]
    private DoorBehavior door;
    private InstantPressurePlate instantPlate;




    void Start()
    {
        door = GetComponentInChildren<DoorBehavior>();
        instantPlate = GetComponentInChildren<InstantPressurePlate>();

    }
    void Update()
    {
        OpenDoorInstantPlate();
    }

    void OpenDoorInstantPlate()
    {
        if (instantPlate.isPressed == true)
        {
            door.isDoorOpen = true;
        }
        else if (instantPlate.isPressed == false)
        {
            door.isDoorOpen = false;
        }
    }


}
