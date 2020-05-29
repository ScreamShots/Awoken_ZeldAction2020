using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerCharge : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject cinemachineAltar = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        cinemachineAltar.SetActive(false);
        playerUI.SetActive(true);

        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.ShieldChargeUnlock;
        GameManager.Instance.areaToLoad = 1;
        ProgressionManager.Instance.PlayerCapacity["Charge"] = true;
        PlayerManager.Instance.ActivateCharge();
        ProgressionManager.Instance.SaveTheProgression();
    }

    [ContextMenu("StartCutSceneCharge")]
    public override void StartCutScene()
    {
        cinemachineAltar.SetActive(true);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
