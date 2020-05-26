using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1ProgressionHandler : BasicProgressionHandler
{
    [Header("Gameplay Elements")]
    [Header("RequieredElements")]

#pragma warning disable
    [SerializeField]
    bool showGameplayElements = false;
    [Space]
#pragma warning restore

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    InstantPressurePlate room1PressurePlate = null;

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    ActionLever room2Lever = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    GameObject room2Cube = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    Vector3 room2CubePosition = new Vector3(0,0,0);

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AreaManager arenaArea = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    ArenaRoom arenaScript = null;

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    GameObject room3Cube = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    Vector3 room3CubePosition = new Vector3(0, 0, 0);
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    StayPressurePlate room3PressurePlate1 = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    StayPressurePlate room3PressurePlate2 = null;

    [Header("CutScene Elements")]

#pragma warning disable
    [SerializeField]
    bool showCutSceneElements = false;
    [Space]

    [SerializeField] [ConditionalHide("showCutSceneElements", true)]
    BasicCutSceneManager CutScene_endFloor1;
#pragma warning restore


    protected override void OnOlympeFloorOneStart()
    {
        CutScene_endFloor1.gameObject.SetActive(true);
    }

    protected override void OnOlympeFloorOneLREntrance()
    {
        room1PressurePlate.isPressed = true;
        room2Lever.isPressed = true;
        room2Cube.transform.position = room2CubePosition;

        arenaArea.canSpawnEnemies = false;
        arenaScript.isEnigmaDone = true;

        room3Cube.transform.position = room3CubePosition;
        room3PressurePlate1.isPressed = true;
        room3PressurePlate2.isPressed = true;
    }



}
