using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossEndGame : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject shield = null;
    [SerializeField]
    GameObject cutSceneUI = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;
    [SerializeField]
    GameObject bossUI = null;
    [SerializeField]
    GameObject realBossRendering = null;

    override public void EndOfCutScene()
    {
        zeus.SetActive(false);
        shield.SetActive(false);
        cutSceneUI.SetActive(false);
        cinemachine.SetActive(false);
        playerUI.SetActive(true);
        bossUI.SetActive(true);

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneBossEndGame")]
    public override void StartCutScene()
    {
        realBossRendering.SetActive(false);
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        zeus.SetActive(true);

        base.StartCutScene();
    }
}
