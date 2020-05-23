using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma2 : EnigmaTool
{
    #region Statements Brazero
    [SerializeField]
    private InstantPressurePlate instantPlate1 = null;
    [SerializeField]
    private ActionLever actionLever1 = null;
    [SerializeField]
    private DoorBehavior door1 = null;
    [SerializeField]
    private GameObject brazero1 = null;
    [SerializeField]
    private GameObject brazero2 = null;

    public bool isBrazeroOn1;
    public bool isBrazeroOn2;
    #endregion

    #region Battle Statement
    [SerializeField]
    private AreaManager autel;
    //[SerializeField]
    //private GameObject chest;
    #endregion

    #region Last Part Statement
    [SerializeField]
    private DistanceLever distanceLever1 = null;
    [SerializeField]
    private DoorBehavior door2 = null;
    [SerializeField]
    private GameObject transitionZone = null;
    #endregion

    public CSTriggerManager csTriggerScriptDoorRight;
    public CSTriggerManager csTriggerScriptLever;
    public CSTriggerManager csTriggerScriptPlate;

    void Awake()
    {
        brazero1.SetActive(false);
        brazero2.SetActive(false);
        //chest.SetActive(false);
    }

    protected override void Start()
    {
    }

    void Update()
    {
        OpenTheDoorAgain();
        //LightBrazero();
        ActivateTransition();
        EnableLightAfterBlending();
        OpenDoorDoublePlate();
    }

    public void OpenDoorDoublePlate()
    {
        if (isBrazeroOn1 == true && isBrazeroOn2 == true)
        {
            door1.isDoorOpen = true;
        }
        else if (isBrazeroOn1 == false || isBrazeroOn2 == false)
        {
            door1.isDoorOpen = false;
        }
    }

    /*void LightBrazero()
    {
        if (instantPlate1.isPressed == true)
        {
            isBrazeroOn1 = true;
        }

        if (actionLever1.isPressed == true)
        {
            isBrazeroOn2 = true;
        }
    }*/

    public void EnableLightAfterBlending()
    {
        if (instantPlate1.isPressed == true)
        {
            if (csTriggerScriptPlate.transitionCamFinish)
            {
                brazero1.SetActive(true);
                isBrazeroOn1 = true;
            }
        }

        if (actionLever1.isPressed == true)
        {
            if (csTriggerScriptLever.transitionCamFinish)
            {
                brazero2.SetActive(true);
                isBrazeroOn2 = true;
            }
        }
    }

    //void UnlockPary()
    //{
    //    if (autel.allEnemyAreDead == true)
    //    {
    //        chest.SetActive(true);
    //    }
    //}

    void OpenTheDoorAgain()
    {
        if (distanceLever1.isPressed == true)
        {
            if (csTriggerScriptDoorRight.transitionCamFinish)
            {
                door2.isDoorOpen = true;
                door1.isDoorOpen = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) //Looks if the Player enters the pressure plate
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            door1.isDoorOpen = false;
            isBrazeroOn1 = false;
            isBrazeroOn2 = false;
            instantPlate1.isPressed = false;
            actionLever1.isPressed = false;
        }
    }

    void ActivateTransition()
    {
        if(door2.isDoorOpen == false)
        {
            transitionZone.SetActive(false);
        }
        else if (door2.isDoorOpen == true)
        {
            transitionZone.SetActive(true);
        }
    }
}
