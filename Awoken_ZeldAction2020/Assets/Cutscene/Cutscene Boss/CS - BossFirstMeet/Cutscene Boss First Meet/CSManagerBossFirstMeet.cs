using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerBossFirstMeet : BasicCutSceneManager
{
    [Header("Zeus Reaveal Specific Elements")]
    [SerializeField]
    GameObject zeus = null;
    [SerializeField]
    GameObject lightning = null;
    [SerializeField]
    GameObject cutSceneUI = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        zeus.SetActive(false);
        lightning.SetActive(false);
        cutSceneUI.SetActive(false);
        cinemachine.SetActive(false);
        playerUI.SetActive(true);

        base.EndOfCutScene();
    }

    [ContextMenu("StartCutSceneBossFirstWalk")]
    public override void StartCutScene()
    {
        cinemachine.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
