using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  EnigmaTool : MonoBehaviour
{
    protected DoorBehavior door;
    protected InstantPressurePlate instantPlate;
    protected StayPressurePlate stayPlate;
    protected ActionLever actionLever;
    [SerializeField]
    protected bool isEnigmaDone;





    protected virtual void Start()
    {
        door = GetComponentInChildren<DoorBehavior>();
        instantPlate = GetComponentInChildren<InstantPressurePlate>();
        stayPlate = GetComponentInChildren<StayPressurePlate>();
        actionLever = GetComponentInChildren<ActionLever>();
    }
    void Update()
    {

    }

    public void opendoorInstantPlate()
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

    public void OpendoorStayPlate()
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

    public void OpenDoorActionLever()
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
