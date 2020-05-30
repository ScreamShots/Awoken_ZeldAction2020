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
    [SerializeField]
    AltarBehaviour thisAltar = null;
    protected override void Start()
    {
        //door1.isDoorOpen = true;
        PlayerManager.Instance.GetComponent<PlayerHealthSystem>().onDead.AddListener(ResetAfterPlayerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        if(ProgressionManager.Instance.thisSessionTimeLine == ProgressionManager.ProgressionTimeLine.ThirdRegionEntrance)
        {
            EnigmaDone();
            OpenTheDoor();
        }
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
        if (isEnigmaDone == true && !thisAltar.buttonActivated)
        {
            thisAltar.buttonActivated = true;
            //door1.isDoorOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Looks if the Player enters the pressure plate
    {
        if(ProgressionManager.Instance.thisSessionTimeLine == ProgressionManager.ProgressionTimeLine.ThirdRegionEntrance)
        {
            if (other.tag == "CollisionDetection" && other.transform.root.tag == "Player")
            {
                door1.isDoorOpen = false;
                activationZone.enabled = false;
            }
        }
    }

    void ResetAfterPlayerDeath()
    {
        door1.isDoorOpen = true;
        activationZone.enabled = true;
    }
}
