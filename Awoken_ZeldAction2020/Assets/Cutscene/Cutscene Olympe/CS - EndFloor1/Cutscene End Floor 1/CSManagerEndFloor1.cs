using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerEndFloor1 : BasicCutSceneManager
{
    [Header("Specific Elements")]
    [SerializeField]
    GameObject cinemachineAltar = null;
    [SerializeField]
    GameObject cinemachine = null;
    [SerializeField]
    GameObject playerUI = null;

    override public void EndOfCutScene()
    {
        cinemachineAltar.SetActive(false);
        cinemachine.SetActive(false);
        playerUI.SetActive(true);

        base.EndOfCutScene();

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorOneLREntrance;
        GameManager.Instance.areaToLoad = 1;
        gameObject.SetActive(false);
        ProgressionManager.Instance.SaveTheProgression();
    }

    [ContextMenu("StartCutSceneStartEndFloor1")]
    public override void StartCutScene()
    {
        cinemachine.SetActive(true);
        cinemachineAltar.SetActive(false);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
