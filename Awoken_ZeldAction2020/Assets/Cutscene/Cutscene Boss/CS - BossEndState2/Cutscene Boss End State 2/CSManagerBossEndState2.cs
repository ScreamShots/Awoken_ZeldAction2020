using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossEndState2 : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeusAniamtor = null;
    [SerializeField]
    GameObject zeusCutSceneAniamtor = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;
    [SerializeField]
    GameObject bossUI = null;
    [SerializeField]
    TransitionArena arenaZone = null;

    override public void EndOfCutScene()
    {
        zeusAniamtor.SetActive(true);
        zeusCutSceneAniamtor.SetActive(false);
        cinemachine.SetActive(false);
        playerUI.SetActive(true);
        bossUI.SetActive(true);
        arenaZone.cutsceneRunning = false;

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneBossEndState2")]
    public override void StartCutScene()
    {
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        zeusCutSceneAniamtor.SetActive(true);
        zeusAniamtor.SetActive(false);
        arenaZone.cutsceneRunning = true;

        base.StartCutScene();
    }
}
