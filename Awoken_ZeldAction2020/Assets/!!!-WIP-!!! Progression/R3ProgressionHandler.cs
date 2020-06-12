﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3ProgressionHandler : BasicProgressionHandler
{
    [Header("GameplayElements")]
    [Header("Requiered Elements")]

    [SerializeField]
#pragma warning disable
    bool showGameplayElements = false;
    [Space]
#pragma warning restore

    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    DoorBehavior doorAutelArea = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    GameObject[] allDestructibleBlocsAfterAutel = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AltarBehaviour thisAltar = null;
    [SerializeField] [ConditionalHide("showGameplayElements", true)]
    AreaManager autelArea = null;
    

    [Header("CutScenes")]

    [SerializeField]
#pragma warning disable
    bool showCutScenesElements = false;
    [Space]
#pragma warning restore

    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    BasicCutSceneManager CutScene_R3Entrance = null;
    [SerializeField] [ConditionalHide("showCutScenesElements", true)]
    BasicCutSceneManager CutScene_UnlockCharge = null;

    protected override void OnOlympeFloorTwoEnd()
    {
        autelArea.canSpawnEnemies = true;
        thisAltar.DesactivateAltarGraph();
        foreach(GameObject destructibleBloc in allDestructibleBlocsAfterAutel)
        {
            destructibleBloc.SetActive(true);
        }
        CutScene_R3Entrance.gameObject.SetActive(true);
        CutScene_UnlockCharge.gameObject.SetActive(true);
        CutScene_R3Entrance.StartCutScene();
        CutScene_R3Entrance.onCutSceneEnd.AddListener(BannerShow);
    }

    protected override void OnThirdRegionEntrance()
    {
        base.OnThirdRegionEntrance();
        autelArea.canSpawnEnemies = true;
        thisAltar.DesactivateAltarGraph();
        foreach (GameObject destructibleBloc in allDestructibleBlocsAfterAutel)
        {
            destructibleBloc.SetActive(true);
        }
        CutScene_UnlockCharge.gameObject.SetActive(true);
    }

    protected override void OnShieldChargeUnlock()
    {
        base.OnShieldChargeUnlock();
        foreach (GameObject destructibleBloc in allDestructibleBlocsAfterAutel)
        {
            destructibleBloc.SetActive(true);
        }
        CutScene_UnlockCharge.gameObject.SetActive(true);

        doorAutelArea.isDoorOpen = false;
    }
}
