using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olympe1Room3 : EnigmaTool
{
    [SerializeField]
    private StayPressurePlate plate1 = null;
    [SerializeField]
    private StayPressurePlate plate2 = null;
    [SerializeField]
    private DoorBehavior door1 = null;

    protected override void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnigmaDone();
        OpenDoor();
    }

    void EnigmaDone()
    {
        if (plate1.isPressed == true && plate2.isPressed == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenDoor()
    {
        if(isEnigmaDone == true)
        {
            door1.isDoorOpen = true;
            plate1.isPressed = true;
            plate2.isPressed = true;
        }
    }
}
