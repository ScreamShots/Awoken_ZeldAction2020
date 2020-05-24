using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossStartBattle : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeusAniamtor = null;
    [SerializeField]
    GameObject zeusCutSceneAniamtor = null;
    [SerializeField]
    GameObject throneCinemachine = null;
    [SerializeField]
    GameObject bossCinemachine = null;
    [SerializeField]
    GameObject playerCinemachine = null;
    [SerializeField]
    GameObject shieldCinemachine = null;
    [SerializeField]
    GameObject playerUI = null;
    [SerializeField]
    GameObject bossUI = null;
    [SerializeField]
    TransitionArena arenaZone = null;
    [SerializeField]
    GameObject gateA = null;
    [SerializeField]
    GameObject gateB = null;

    override public void EndOfCutScene()
    {
        zeusAniamtor.SetActive(true);
        zeusCutSceneAniamtor.SetActive(false);
        bossCinemachine.SetActive(true);
        playerCinemachine.SetActive(false);
        shieldCinemachine.SetActive(false);
        throneCinemachine.SetActive(false);
        playerUI.SetActive(true);
        bossUI.SetActive(true);
        arenaZone.cutsceneRunning = false;

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneStartBossBattle")]
    public override void StartCutScene()
    {
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        zeusCutSceneAniamtor.SetActive(true);
        zeusAniamtor.SetActive(false);
        arenaZone.cutsceneRunning = true;
        gateA.SetActive(false);
        gateB.SetActive(false);
        playerCinemachine.SetActive(true);

        base.StartCutScene();
    }
}
