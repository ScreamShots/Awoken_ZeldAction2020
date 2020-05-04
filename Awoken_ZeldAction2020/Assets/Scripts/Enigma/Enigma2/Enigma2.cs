using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma2 : EnigmaTool
{
    #region Statements Brazero
    [SerializeField]
    private InstantPressurePlate instantPlate1;
    [SerializeField]
    private ActionLever actionLever1;
    [SerializeField]
    private DoorBehavior door1;
    [SerializeField]
    private GameObject brazero1;
    [SerializeField]
    private GameObject brazero2;
    #endregion

    #region Battle Statement
    [SerializeField]
    private AreaManager autel;
    //[SerializeField]
    //private GameObject chest;
    #endregion

    #region Last Part Statement
    [SerializeField]
    private DistanceLever distanceLever1;
    [SerializeField]
    private DoorBehavior door2;
    #endregion

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
        OpenDoorDoublePlate();
        OpenTheDoorAgain();
        LightBrazero();
    }

    public void OpenDoorDoublePlate()
    {
        if (instantPlate1.isPressed == true && actionLever1.isPressed == true)
        {
            door1.isDoorOpen = true;
        }
        else if (instantPlate1.isPressed == false || actionLever1.isPressed == false)
        {
            door1.isDoorOpen = false;
        }
    }

    void LightBrazero()
    {
        if (instantPlate1.isPressed == true)
        {
            brazero1.SetActive(true);
        }

        if (actionLever1.isPressed == true)
        {
            brazero2.SetActive(true);
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
            door2.isDoorOpen = true;
        }
    }

}
