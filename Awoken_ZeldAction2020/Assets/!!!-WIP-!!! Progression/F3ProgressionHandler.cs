using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F3ProgressionHandler : BasicProgressionHandler
{
    [Header("GamePlay Elements")]
    [Header("Requiered Elements")]

#pragma warning disable 
    [SerializeField]
    bool showGameplayElements = false;
    [Space]
#pragma warning restore 

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    DoorBehavior room2Door1 = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    DoorBehavior room2Door2 = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    ActionLever room2ActionLever = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    InstantPressurePlate room2PressurePlate = null;

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AreaManager areaRight = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AreaManager areaLeft = null;

    [Header("CutSceneElemenents")]

#pragma warning disable 
    [SerializeField]
    bool showCutSceneElements = false;
    [Space]
#pragma warning restore 

    [SerializeField] [ConditionalHide("showCutSceneElements", true)]
    BasicCutSceneManager CutScene_LastRoom = null;

    protected override void OnOlympeFloorThreeStart()
    {
        CutScene_LastRoom.gameObject.SetActive(true);
    }

    protected override void OnOlympeFloorThreeLREntrance()
    {
        GameManager.Instance.areaToLoad = 1;
        room2ActionLever.isPressed = true;
        room2PressurePlate.isPressed = true;
        room2Door1.isDoorOpen = true;
        room2Door2.isDoorOpen = true;

        areaRight.canSpawnEnemies = false;
        areaLeft.canSpawnEnemies = false;
    }

}
