using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olympe2Room3 : EnigmaTool
{
    [SerializeField]
    private StayPressurePlate plate1;
    [SerializeField]
    private StayPressurePlate plate2;
    [SerializeField]
    private StayPressurePlate plate3;
    [SerializeField]
    private StayPressurePlate plate4;
    [SerializeField]
    private DistanceLever lever1;
    [SerializeField]
    private DoorBehavior door1;
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
        if(plate1.isPressed == true && plate2.isPressed == true && plate3.isPressed == true && plate4.isPressed == true && lever1.isPressed == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenDoor()
    {
        if(isEnigmaDone == true)
        {
            door1.isDoorOpen = true;
        }
    }

}
