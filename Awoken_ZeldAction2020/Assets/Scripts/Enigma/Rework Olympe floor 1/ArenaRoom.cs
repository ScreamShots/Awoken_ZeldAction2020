using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRoom : EnigmaTool
{
    [SerializeField]
    private AreaManager arena;
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
        if (arena.allEnemyAreDead == true)
        {
            isEnigmaDone = true;
        }
    }

    void OpenDoor()
    {
        if (isEnigmaDone == true)
        {
            door1.isDoorOpen = true;
        }
    }
}
