using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script made by Bastien Prigent
/// THis is the script of the first Enigma of our game
/// </summary>
public class Enigma1Part2 : MonoBehaviour
{
    [SerializeField]
    private DoorBehavior door;
    private StayPressurePlate stayPlate;



    void Start()
    {
        door = GetComponentInChildren<DoorBehavior>();
        stayPlate = GetComponentInChildren<StayPressurePlate>();
    }
    void Update()
    {
        OpenDoorStayPlate();
    }


    void OpenDoorStayPlate()
    {
        if (stayPlate.isPressed == true)
        {
            door.isDoorOpen = true;
        }
        else if (stayPlate.isPressed == false)
        {
            door.isDoorOpen = false;
        }
    }



}

