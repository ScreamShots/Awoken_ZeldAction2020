using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigma3 : EnigmaTool
{
    [SerializeField]
    private AreaManager autel = null;
    [SerializeField]
    private DoorBehavior door1 = null;
    [SerializeField]
    Collider2D activationZone = null;
    protected override void Start()
    {
        door1.isDoorOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnigmaDone();
        OpenTheDoor();
    }
    void EnigmaDone()
    {
        if(autel.allEnemyAreDead == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenTheDoor()
    {
        if (isEnigmaDone == true)
        {
            door1.isDoorOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Looks if the Player enters the pressure plate
    {
        if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
        {
            door1.isDoorOpen = false;
            activationZone.enabled =false;
        }
    }
}
