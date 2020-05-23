using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  EnigmaTool : MonoBehaviour
{
    protected DoorBehavior door;
    protected InstantPressurePlate instantPlate;
    protected StayPressurePlate stayPlate;
    protected ActionLever actionLever;
    protected DistanceLever distanceLever;
    [SerializeField]
    protected bool isEnigmaDone;

    public bool transiCam = false;
    CSTriggerManager csTriggerManagerScript;



    protected virtual void Start()
    {
        door = GetComponentInChildren<DoorBehavior>();
        instantPlate = GetComponentInChildren<InstantPressurePlate>();
        stayPlate = GetComponentInChildren<StayPressurePlate>();
        actionLever = GetComponentInChildren<ActionLever>();
        distanceLever = GetComponentInChildren<DistanceLever>();

        if (transiCam)
        {
            csTriggerManagerScript = GetComponent<CSTriggerManager>();
        }
    }

    public void opendoorInstantPlate()
    {
        if (instantPlate.isPressed == true)
        {
            if (transiCam)
            {
                if (csTriggerManagerScript.transitionCamFinish)
                {
                    door.isDoorOpen = true;
                }
            }
            else
            {
                door.isDoorOpen = true;
            }
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
            if (transiCam)
            {
                if (csTriggerManagerScript.transitionCamFinish)
                {
                    door.isDoorOpen = true;
                }
            }
            else
            {
                door.isDoorOpen = true;
            }
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
            if (transiCam)
            {
                if (csTriggerManagerScript.transitionCamFinish)
                {
                    door.isDoorOpen = true;
                }
            }
            else
            {
                door.isDoorOpen = true;
            }
        }
        else if (actionLever.isPressed == false)
        {
            door.isDoorOpen = false;
        }
    }

    public void OpenDoorDistanceLever()
    {
        if (distanceLever.isPressed == true)
        {
            if (transiCam)
            {
                if (csTriggerManagerScript.transitionCamFinish)
                {
                    door.isDoorOpen = true;
                }
            }
            else
            {
                door.isDoorOpen = true;
            }
        }
        else if (distanceLever.isPressed == false)
        {
            door.isDoorOpen = false;
        }
    }
}
