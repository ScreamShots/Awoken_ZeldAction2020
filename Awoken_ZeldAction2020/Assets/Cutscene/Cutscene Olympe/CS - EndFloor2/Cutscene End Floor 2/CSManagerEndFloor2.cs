using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSManagerEndFloor2 : BasicCutSceneManager
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

        ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.OlympeFloorTwoLREntrance;
        GameManager.Instance.areaToLoad = 1;
        gameObject.SetActive(false);
    }

    [ContextMenu("StartCutSceneStartEndFloor2")]
    public override void StartCutScene()
    {
        cinemachine.SetActive(true);
        cinemachineAltar.SetActive(false);
        playerUI.SetActive(false);

        base.StartCutScene();
    }
}
