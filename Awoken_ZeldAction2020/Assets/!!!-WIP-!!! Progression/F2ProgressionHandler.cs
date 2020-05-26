using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F2ProgressionHandler : BasicProgressionHandler
{
    [Header("Gameplay Elements")]
    [Header("Requiered Elements")]

#pragma warning disable
    [SerializeField]
    bool canShowGameplayElements = false;
    [Space]
#pragma warning restore

    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Olympe2Room1 room1Script = null;


    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    GameObject room2MoovingTurret = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    DistanceLever room2DistanceLever = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Vector3 room2MoovingTurretPos = new Vector3(0, 0, 0);

    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    GameObject room3Cube1 = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    GameObject room3Cube2 = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    GameObject room3Cube3 = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    GameObject room3MoovingTurret = null;
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    DistanceLever room3DistanceLever = null;

    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Vector3 room3Cube1Pos = new Vector3(0, 0, 0);
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Vector3 room3Cube2Pos = new Vector3(0, 0, 0);
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Vector3 room3Cube3Pos = new Vector3(0, 0, 0);
    [SerializeField] [ConditionalHide("canShowGameplayElements", true)]
    Vector3 room3MoovingTurretPos = new Vector3(0, 0, 0);

    [Header("CutScene Elements")]

#pragma warning disable
    [SerializeField]
    bool canShowCutSceneElements = false;
    [Space]
#pragma warning restore

    [SerializeField] [ConditionalHide("canShowCutSceneElements", true)]
    BasicCutSceneManager CutScene_LastRoomCutscene = null;

    protected override void OnOlympeFloorTwoStart()
    {
        CutScene_LastRoomCutscene.gameObject.SetActive(true);
    }

    protected override void OnOlympeFloorTwoLREntrance()
    {
        CutScene_LastRoomCutscene.gameObject.SetActive(false);

        room1Script.DoTheEnigma();

        room2MoovingTurret.transform.position = room2MoovingTurretPos;
        room2DistanceLever.isPressed = true;

        room3Cube1.transform.position = room3Cube1Pos;
        room3Cube2.transform.position = room3Cube2Pos;
        room3Cube3.transform.position = room3Cube3Pos;
        room3MoovingTurret.transform.position = room3MoovingTurretPos;
        room3DistanceLever.isPressed = true;

    }
}
