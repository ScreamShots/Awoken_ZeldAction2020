using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlympeFloor1Enigma1 : EnigmaTool
{
    [SerializeField]
    private InstantPressurePlate instantPlate1;
    [SerializeField]
    private InstantPressurePlate instantPlate2;
    [SerializeField]
    private DoorBehavior door1;

    protected override void Start()
    {

    }

    void Update()
    {
        OpenTheDoor();
    }

    void OpenTheDoor()
    {
        if (instantPlate1.isPressed == true && instantPlate2.isPressed == true)
        {
            door1.isDoorOpen = true;
        }
    }
}
