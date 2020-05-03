using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma2 : EnigmaTool
{
    #region Statements
    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private InstantPressurePlate instantPlate1;
    [SerializeField]
    private InstantPressurePlate instantPlate2;
    [SerializeField]
    private DoorBehavior door1;
    [SerializeField]
    private DoorBehavior door2;
    [SerializeField]
    private ActionLever actionLever1;
    [SerializeField]
    private GameObject[] pressurePlates = new GameObject[3];
    [SerializeField]
    private BoxCollider2D activationZone;
    #endregion

    void Awake()
    {
        instantPlate1.isPressed = true;
        instantPlate2.isPressed = true;
        actionLever1.isPressed = true;

        isEnigmaDone = false;
        for (int i = 0; i < pressurePlates.Length; i++)
        {
            pressurePlates[i].SetActive(false);
        }
    }

    protected override void Start()
    {
       
    }

    void Update()
    {
        OpenDoorDoublePlate();
    }

    public void OpenDoorDoublePlate()
    {
        if (instantPlate1.isPressed == true && instantPlate2.isPressed == true && actionLever1.isPressed == true && isEnigmaDone == false)
        {
            door1.isDoorOpen = true;
            door2.isDoorOpen = true;
            isEnigmaDone = true;
        }
        else if (instantPlate1.isPressed == false && isEnigmaDone == false || instantPlate2.isPressed == false && isEnigmaDone == false)
        {
            door1.isDoorOpen = false;
            door2.isDoorOpen = false;
            isEnigmaDone = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            instantPlate1.isPressed = false;
            instantPlate2.isPressed = false;
            actionLever1.isPressed = false;
            isEnigmaDone = false;
            for (int i = 0; i < pressurePlates.Length; i++)
            {
                pressurePlates[i].SetActive(true);
            }
            activationZone.enabled = false;
        }
    }
}
