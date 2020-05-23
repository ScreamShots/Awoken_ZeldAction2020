using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoomOlympe3 : EnigmaTool
{
    [SerializeField]
    private AreaManager area3;
    [SerializeField]
    private AreaManager area4;
    [SerializeField]
    private DoorBehavior door1;
    [SerializeField]
    private DoorBehavior door2;
    [SerializeField]
    private InstantPressurePlate plate1;
    [SerializeField]
    private ActionLever lever1;
    [SerializeField]
    private DoorBehavior finalDoor;
    protected override void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenDoor1();
        OpenDoor2();
        EnigmaDone();
        OpenFinalDoor();
    }

    void OpenDoor1()
    {
        if(area3.allEnemyAreDead == true)
        {
            door1.isDoorOpen = true;
        }
    }

    void OpenDoor2()
    {
        if(area4.allEnemyAreDead == true)
        {
            door2.isDoorOpen = true;
        }
    }

    void EnigmaDone()
    {
        if(plate1 == true && lever1 == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenFinalDoor()
    {
        if(isEnigmaDone == true)
        {
            finalDoor.isDoorOpen = true;
        }
    }
}
