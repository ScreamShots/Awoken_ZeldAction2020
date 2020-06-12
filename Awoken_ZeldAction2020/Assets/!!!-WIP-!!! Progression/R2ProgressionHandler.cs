using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2ProgressionHandler : BasicProgressionHandler
{
    [Header("GameplayElements")]
    [Header("Requiered Elements")]

    [SerializeField]
#pragma warning disable
    bool showGameplayElements = false;
#pragma warning restore
    [Space]

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    ActionLever brazeroLever = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    InstantPressurePlate brazeroPlate = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    Enigma2 enigmaParyScript = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AltarBehaviour altar = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    DistanceLever pipeEnigmaLever = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    ActionLever shortCutLever = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    DoorBehavior arenaDoorOut = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AreaManager autelArea = null;

    [Header("CutScenes")]

    [SerializeField]
#pragma warning disable
    bool showCutScenesElements = false;
#pragma warning restore
    [Space]

    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    GameObject CutScene_R2Entrance = null;
    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    GameObject CutScene_GetPary = null; 
    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    CSTriggerManager brazeroCam1 = null;
    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    CSTriggerManager brazeroCam2 = null;
    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    CSTriggerManager pipeEnigmaCam = null;

    protected override void OnOlympeFloorOneEnd()
    {
        autelArea.canSpawnEnemies = true;
        CutScene_R2Entrance.SetActive(true);
        CutScene_GetPary.SetActive(true);
        brazeroCam1.shortCutByProgression = false;
        brazeroCam2.shortCutByProgression = false;
        pipeEnigmaCam.shortCutByProgression = false;
        brazeroLever.isPressed = false;
        brazeroPlate.isPressed = false;
        enigmaParyScript.mustActivateAltar = true;
        enigmaParyScript.activatePlayerDetection = true;
        altar.DesactivateAltarGraph();
        pipeEnigmaLever.isPressed = false;
        shortCutLever.isPressed = false;
        arenaDoorOut.isDoorOpen = false;

       CutScene_R2Entrance.GetComponent<BasicCutSceneManager>().StartCutScene();
        CutScene_R2Entrance.GetComponent<BasicCutSceneManager>().onCutSceneEnd.AddListener(BannerShow);

    }

    protected override void OnSecondRegionEntrance()
    {
        base.OnSecondRegionEntrance();
        autelArea.canSpawnEnemies = true;
        CutScene_GetPary.SetActive(true);
        brazeroCam1.shortCutByProgression = false;
        brazeroCam2.shortCutByProgression = false;
        pipeEnigmaCam.shortCutByProgression = false;
        brazeroLever.isPressed = false;
        brazeroPlate.isPressed = false;
        enigmaParyScript.mustActivateAltar = true;
        enigmaParyScript.activatePlayerDetection = true;
        altar.DesactivateAltarGraph();
        pipeEnigmaLever.isPressed = false;
        shortCutLever.isPressed = false;
        arenaDoorOut.isDoorOpen = false;

    }

    protected override void OnSecondRegionBrazeros()
    {
        base.OnSecondRegionBrazeros();
        autelArea.canSpawnEnemies = true;
        CutScene_GetPary.SetActive(true);
        pipeEnigmaCam.shortCutByProgression = false;
        enigmaParyScript.mustActivateAltar = true;
        enigmaParyScript.activatePlayerDetection = true;
        altar.DesactivateAltarGraph();
        pipeEnigmaLever.isPressed = false;
        shortCutLever.isPressed = false;
        arenaDoorOut.isDoorOpen = false;
    }

    protected override void OnShieldParyUnlocked()
    {
        base.OnShieldParyUnlocked();
        enigmaParyScript.forceClose = true;
        pipeEnigmaCam.shortCutByProgression = false;
        pipeEnigmaLever.isPressed = false;
        shortCutLever.isPressed = false;
        arenaDoorOut.isDoorOpen = false;
    }

}
